using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour {

    //use to find the fishmanager
    [SerializeField]
    private string fishManagerTag;

    [SerializeField]
    private Transform neighbor;

    //use to save the original pos for penetration and reset
	private Vector3 originalPos;

    //use to save the fishmanager
    private GameObject fishManager;

    private Vector3 neighborPos;



	void Start()
    {
        neighborPos = neighbor.position;
		originalPos = transform.position;

        fishManager = GameObject.FindWithTag(fishManagerTag);
    }

    //use to avoid penetration with table
    private void FixedUpdate()
    {
        if(transform.position.y < originalPos.y)
        {
            transform.position += new Vector3(0.0f, originalPos.y - transform.position.y, 0.0f);
        }
        if (transform.position.y > 1)
        {
            Reset();
        }
    }

    //use to reset the pan when release
    public void Reset()
    {
        if(Vector3.Distance(transform.position,originalPos)>Vector3.Distance(transform.position,neighborPos))
            transform.position = neighborPos;
        else
		    transform.position = originalPos;
        if (fishManager.GetComponent<FishManager>().GetResetSign())
        {
            fishManager.GetComponent<FishManager>().InstantiateFish();
        }
	}
    
}
