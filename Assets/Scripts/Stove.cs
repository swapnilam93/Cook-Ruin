using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour {

    [SerializeField]
    private string stoveTag;

    [SerializeField]
    private string tableTag;

    [SerializeField]
    private GameObject stoveFire;

    private int status = 0;

	// Use this for initialization
	void Start () {
        //StartCoroutine(LightUp());
	}

    private void Update()
    {
        //if(status == 3)
        //{
        //    gameObject.tag = stoveTag;
        //    stoveFire.SetActive(true);
        //}
        //else
        //{
        //    gameObject.tag = tableTag;
        //    stoveFire.SetActive(false);
        //}
    }

    IEnumerator LightUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            if(status < 3)
                status++;
        }
    }
}
