using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

    [SerializeField]
    private float minimumY;

    [SerializeField]
    private string dishTag;

    [SerializeField]
    private AudioClip dropInPotClip;

    private AudioSource dropinPotSound;

    private bool onGrab = true;

    private bool firstIn = true;

    private bool inPot = false;
    private bool inPotPlayed = false;

    GranyAController granyAController;

    void Awake() {
    }

    private void Start()
    {
        dropinPotSound = GetComponent<AudioSource>();
        granyAController = GameObject.Find("GranyA").GetComponent<GranyAController>();
        
    }

    private void Update()
    {
        if (transform.position.y < minimumY)
            Invoke("SelfDestroy",2.0f);
    }

    public void OnGrab()
    {
        onGrab = false;
    }

    private void SelfDestroy()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(dishTag) && !inPot)
        {
            inPot = true;
            other.GetComponent<DishManager>().IngredientIn(gameObject.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(dishTag) & inPot)
        {
            inPot = false;
            other.GetComponent<DishManager>().IngredientOut(gameObject.tag);
            inPotPlayed = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(inPot && !inPotPlayed) {
            dropinPotSound.PlayOneShot(dropInPotClip);
            inPotPlayed = true;
            granyAController.Smile();
            if (GameObject.FindWithTag(dishTag).GetComponent<DishManager>().GetWinStatus())
            {
                GameObject.FindWithTag(dishTag).GetComponent<DishManager>().TriggerWin();
            }
            GameObject.Find("Task1Instruction").GetComponent<Task1Instruction>().StopInstruction();
        }
    }

    public bool GetInPot() {
        return inPot;
    }
    public void AnimateSqueeze() {
        this.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void AnimateRelease() {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void ResetInstruction()
    {
        GameObject.Find("Task1Instruction").GetComponent<Task1Instruction>().ResetInstruction();
    }

    public void NextInstruction()
    {
        GameObject.Find("Task1Instruction").GetComponent<Task1Instruction>().NextInstruction();
    }
}
