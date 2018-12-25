using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBurned : MonoBehaviour {

    //use to find the fishmanager
    [SerializeField]
    private string fishManagerTag;

    [SerializeField]
    private string plateTag;

    [SerializeField]
    private AudioClip ignition;



    //use to save the fishmanager
    private GameObject fishManager;

    private GameObject plate;

    private AudioSource fishBurnedSound;

    // Use this for initialization
    void Start () {
        GameObject.Find("GranyA").GetComponent<GranyAController>().Smile();
        fishBurnedSound = gameObject.GetComponent<AudioSource>();
        plate = GameObject.FindWithTag(plateTag);
        fishManager = GameObject.FindWithTag(fishManagerTag);
        StartCoroutine(WaitForFlame());
    }
	
    IEnumerator WaitForFlame()
    {
        fishBurnedSound.PlayOneShot(ignition);
        yield return new WaitForSeconds(2.0f);
        fishManager.GetComponent<FishManager>().ResetFish();
        gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        gameObject.transform.position = plate.GetComponent<Plate>().InPlate();
    }

}
