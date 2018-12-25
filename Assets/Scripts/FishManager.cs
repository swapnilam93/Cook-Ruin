using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour {
    

    [SerializeField]
    private GameObject pan;

    [SerializeField]
    private Transform task2;

    //use to instantiate fish
    [SerializeField]
    private GameObject fish;


    //sign to reset by pan
    private bool waitForReset = false;
    

    //reset by fish
	public void ResetFish()
    {
        waitForReset = true;         
    }


    //instantiate
    public void InstantiateFish()
    {
        GameObject newFish = GameObject.Instantiate(fish,task2);
        newFish.transform.position = transform.position;
    }

    //reset by pan
    public bool GetResetSign()
    {
        if (waitForReset)
        {
            waitForReset = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReduceFishUI() {
        GameObject.Find("fishUIBoard").GetComponent<UIScoreManager>().DestroyFishUI();
    }

}
