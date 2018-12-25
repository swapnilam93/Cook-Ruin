// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MonoBehaviour {

    [Range(0f,10f)]
	public float moveSpeed = 4f;  // enemy move speed when moving

	public GameObject[] myWaypoints; // to define the movement waypoints
	
    [Tooltip("How much time in seconds to wait at each waypoint.")]
	public float waitAtWaypointTime = 1f;   // how long to wait at a waypoint
	
	public bool loopWaypoints = true; // should it loop through the waypoints

	[SerializeField]
    public string objectTag; // to check the peppers in the pot
	[SerializeField]
    public string managerTag; // to subtract pepper count from the pot

    [SerializeField]
    private AudioClip[] grannyInterfere;

    [SerializeField]
    private int taskNo;

    private AudioSource _audio;

    // SFXs
    // private variables below
    // store references to components on the gameObject
    Transform _transform;
	Rigidbody _rigidbody;
	Animator _animator;

	// movement tracking
    [SerializeField]
	int _myWaypointIndex = 0; // used as index for My_Waypoints
	float _moveTime;
	float _vx = 0f;
	bool _moving = true;
	bool _destroyed = false; // to keep a check of enemy destroying one pepper at a time
	DishManager dishManager; // reference to dish manager to update pepper count and music
    FishManager fishManager;
	[SerializeField]
	float _shakeTime; // time for which enemy shakes

    int caughtTime = 0;

	GranyAController granyAController;

    void Awake()
	{
		_transform = GetComponent<Transform> ();
		_rigidbody = GetComponent<Rigidbody> ();
		if (_rigidbody==null) // if Rigidbody is missing
			Debug.LogError("Rigidbody2D component missing from this gameobject");
		
		_animator = GetComponent<Animator>();
		if (_animator==null) // if Animator is missing
			Debug.LogError("Animator component missing from this gameobject");

        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        { // if AudioSource is missing
            Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
            //let's just add the AudioSource component dynamically

            _audio = gameObject.AddComponent<AudioSource>();
        }
        _moveTime = 0f;
		_moving = true;
        if(taskNo==1)
		    dishManager = GameObject.FindGameObjectWithTag(managerTag).GetComponent<DishManager>();
        else if (taskNo == 2)
            fishManager = GameObject.FindGameObjectWithTag(managerTag).GetComponent<FishManager>();
        granyAController = GameObject.Find("GranyA").GetComponent<GranyAController>();
	}

	void Update () {
		if (Time.time >= _moveTime) {
			_destroyed = false;
			EnemyMovement();
		} else {
			if (Mathf.Abs(transform.position.z - myWaypoints[0].transform.position.z) < 0.1f) {
                if (taskNo == 1)
                    CheckAndDestroyPepper();
                else if (taskNo == 2)
                    CheckAndDestroyFish();
			}
			_animator.SetBool("Walking", false);
			_animator.SetBool("Breath", true);
		}
	}
	
	// Move the enemy through its rigidbody based on its waypoints
	void EnemyMovement() {
		// if there isn't anything in My_Waypoints
		if ((myWaypoints.Length != 0) && (_moving)) {
			
			// make sure the enemy is facing the waypoint (based on previous movement)
			Flip (_vx);
			
			// determine distance between waypoint and enemy
			_vx = myWaypoints[_myWaypointIndex].transform.position.z-_transform.position.z;
			
			// if the enemy is close enough to waypoint, make it's new target the next waypoint
			if (Mathf.Abs(_vx) <= 0.05f) {
				// At waypoint so stop moving
				_rigidbody.velocity = new Vector3(0, 0, 0);
				
				// increment to next index in array
				_myWaypointIndex++;
				
				// reset waypoint back to 0 for looping
				if(_myWaypointIndex >= myWaypoints.Length) {
					if (loopWaypoints)
						_myWaypointIndex = 0;
					else
						_moving = false;
				}

				// setup wait time at current waypoint
				_moveTime = Time.time + waitAtWaypointTime;
			} else {
				// enemy is moving
				_animator.SetBool("Walking", true);
				_animator.SetBool("Breath", false);
				
				// Set the enemy's velocity to moveSpeed in the x direction.
				_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _transform.localScale.z * moveSpeed);
			}
			
		}
	}

	// flip the enemy to face torward the direction he is moving in
	void Flip(float _vx) {
		
		// get the current scale
		Vector3 localScale = _transform.localScale;
		
		if ((_vx>0f)&&(localScale.z<0f))
			localScale.z*=-1;
		else if ((_vx<=0f)&&(localScale.z>0f))
			localScale.z*=-1;
		
		// update the scale
		_transform.localScale = localScale;
	}

	// check if pepper is present in pot when enemy is at 0 position and destroy one
	private void CheckAndDestroyPepper() {
		GameObject[] peppers = GameObject.FindGameObjectsWithTag(objectTag);
		foreach(GameObject pepper in peppers) {
			if(!_destroyed && pepper.GetComponent<Ingredient>().GetInPot()) {
				// shake grany B
				StartCoroutine(ShakeGranny());
				// animate panic in grany A
				granyAController.Scared();
				granyAController.AnimatePanic();
				Debug.Log("destroy");
                // destroy pepper
                //Debug.Log(pepper.GetComponent<Material>());
                StartCoroutine(Dissolve(pepper));
				//GameObject.Destroy(pepper);
				// to destroy 1 pepper at a time
				_destroyed = true;
				// reduce pepper count
				dishManager.IngredientOut(pepper.tag);
				_animator.SetBool("Breath", true);
			}
		}
	}

    private void CheckAndDestroyFish()
    {
        GameObject[] fishes = GameObject.FindGameObjectsWithTag(objectTag);
        foreach (GameObject fish in fishes)
        {
            if (!_destroyed && fish.GetComponent<Fish>().GetInFire())
            {
                fish.tag = "Untagged";
                fish.GetComponent<Fish>().Destruction();
                StartCoroutine(ShakeGranny());
                granyAController.Scared();
                granyAController.AnimatePanic();
                Debug.Log("destroy");
                //GameObject.Destroy(fish);
                StartCoroutine(Dissolve(fish));
                _destroyed = true;
				fishManager.ReduceFishUI();
                _animator.SetBool("Breath", true);
            }
        }
    }

	// shake enemy while destroying the pepper
	IEnumerator ShakeGranny() {
        _animator.SetTrigger("Shaking");
        Debug.Log("caught"+caughtTime);
        _audio.PlayOneShot(grannyInterfere[caughtTime++]);
        if (caughtTime > 2)
            caughtTime = 2;
        yield return new WaitForSeconds(_shakeTime);
	}

	public void SetMoveSpeed(float newSpeed) {
		moveSpeed = newSpeed;
	}

    IEnumerator Dissolve(GameObject obj)
    {
        float t = 0.0f;
        while (t <= 2.0f)
        {
            obj.GetComponent<MeshRenderer>().material.SetFloat("_DissolveThreshold", t / 2f);
            t += Time.deltaTime;
            yield return null;
        }
        GameObject.Destroy(obj);
        fishManager.ResetFish();
    }
    
}