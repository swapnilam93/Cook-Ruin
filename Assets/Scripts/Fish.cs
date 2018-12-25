using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    //use to find the pan
	[SerializeField]
	private string panTag;

    //use to find the fishmanager
    [SerializeField]
    private string fishManagerTag;

    //use to determine the destroy state
    [SerializeField]
    private float minimumY;

    //use to find the table
    [SerializeField]
    private string tableTag;

    //use to find the stove
    [SerializeField]
    private string stoveTag;

    //use to determine the "fake" flip height
    [SerializeField]
    private float flipForce;

    [SerializeField]
    private GameObject fishBurned;

    [SerializeField]
    private float panRadius;

    [SerializeField]
    private float burningTime;

    [SerializeField]
    private int state;

    [SerializeField]
    private string taskName;

    [SerializeField]
    private string plateTag;

    [SerializeField]
    private string flame;

    //use to save the offset of the height of pan and fish
    private float offset;

    //use to save the pan
    private GameObject pan;

    //use to save the fishmanager
    private GameObject fishManager;

    private bool onBuringProcess = false;

    private Vector3 lastPan;

    private float lastHeightOffset;

    private AudioSource fishSound;

    private float timer = 0.0f;

    private Transform task2;

    private GameObject plate;

    private UIScoreManager UIScoreManager;

    private bool inDestruction = false;

    //initializing
	void Awake(){
        fishSound = gameObject.GetComponent<AudioSource>();
        if (fishSound == null)
        { // if AudioSource is missing
            Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
            //let's just add the AudioSource component dynamically

            fishSound = gameObject.AddComponent<AudioSource>();
        }
        pan = GameObject.FindWithTag(panTag);
        fishManager = GameObject.FindWithTag(fishManagerTag);
        lastHeightOffset = 0.0f;
        task2 = GameObject.Find(taskName).transform;
        plate = GameObject.FindWithTag(plateTag);
        UIScoreManager = GameObject.Find("fishUIBoard").GetComponent<UIScoreManager>();
        if (UIScoreManager==null) // if UIScoreManager is missing
			Debug.LogError("UIScoreManager component missing from this gameobject");
    }

    private void Start()
    {
        offset = pan.transform.position.y - transform.position.y;
        lastPan = new Vector3(pan.transform.position.x, -10.0f, pan.transform.position.z);
    }

    //use to determine the destroy
    private void Update()
    {
        if (transform.position.y < minimumY)
        {
            StartCoroutine(SelfDestroy(false));
        }
        if (timer >= burningTime)
        {
            StartCoroutine(SelfDestroy(true));
        }
    }

    //destroy
    IEnumerator SelfDestroy(bool success)
    {
        if (success)
        {
            yield return null;
            Vector3 tmpPos = transform.position;
            Quaternion tmpRot = transform.rotation;
            GameObject.Destroy(gameObject);
            GameObject burned = GameObject.Instantiate(fishBurned,task2);
            burned.transform.position = tmpPos;
            burned.transform.rotation = tmpRot;
            Debug.Log("NextState");

            //update fish UI
            UIScoreManager.UpdateFishUI();

            if (state == 1)
            {
                GameObject.FindWithTag(flame).GetComponent<ParticleSystem>().Play();
            }
        }
        else if(!onBuringProcess)
        {
            yield return new WaitForSeconds(2.0f);
            GameObject.Destroy(gameObject);
            UIScoreManager.DestroyFishUI();
            fishManager.GetComponent<FishManager>().ResetFish();
        }
    }

    //use to fix the penetration
    void FixedUpdate(){

		float f = transform.position.y + offset;
		float p = pan.transform.position.y;
        Vector3 curPan = new Vector3(pan.transform.position.x, 0.0f, pan.transform.position.z);
        Vector3 dir = curPan - new Vector3(transform.position.x,0.0f,transform.position.z);
        float d1 = curPan.x - transform.position.x;
        float d2 = curPan.z - transform.position.z;
        float d = dir.magnitude;
        if (lastHeightOffset >= -0.02f && f <= p && d <= panRadius)
        {
            transform.position += new Vector3(0.0f, (p - f) * (p - f) * flipForce, 0.0f);
            lastHeightOffset = transform.position.y + offset - p;
            if (lastHeightOffset < 0)
            {
                transform.position -= new Vector3(0.0f, lastHeightOffset, 0.0f);
            }
        }
        if (lastHeightOffset >= -0.02f && f - p < 0.1f && d <= panRadius)
        {
            if (lastPan.y > -5)
            {
                transform.position += curPan - lastPan;
            }
            lastPan = curPan;
        }
        else
        {
            lastPan.y = -10;
        }
        lastHeightOffset = transform.position.y + offset - p;
    }

    //use to determin the destroy
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(panTag))
        {
            StartCoroutine(SelfDestroy(false));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(stoveTag))
        {
            onBuringProcess = true;
            fishSound.Play();
            GameObject.Find("Task2Instruction").GetComponent<Task2Instruction>().StopInstruction();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(!inDestruction && other.CompareTag(stoveTag))
        {
            timer += Time.deltaTime;
        }
        else if(!inDestruction && other.CompareTag(tableTag) && state == 0)
        {
            timer += Time.deltaTime/5;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(stoveTag))
        {
            onBuringProcess = false;
            fishSound.Stop();
        }
    }

    public bool GetInFire()
    {
        return onBuringProcess;
    }

    public void Destruction()
    {
        inDestruction = true;
    }

}
