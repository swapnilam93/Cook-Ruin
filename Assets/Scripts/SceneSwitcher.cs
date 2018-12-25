using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour {

	[SerializeField]
	GameObject[] scenes;

	private int sceneNo;

	void Awake() {
		sceneNo = 0;
	}

	// Use this for initialization
	void Start () {
		//scenes[sceneNo].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartNextScene() {
		Debug.Log("scene " + sceneNo + " " + scenes[sceneNo].name);
		//foreach(GameObject scene in scenes) {
			scenes[sceneNo].SetActive(false);
			sceneNo++;
			scenes[sceneNo].SetActive(true);
		//}*
	}
}
