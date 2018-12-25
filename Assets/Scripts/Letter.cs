using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Letter : MonoBehaviour {

	[SerializeField]
	GameObject envelope;

	[SerializeField]
	GameObject letter;

	[SerializeField]
	GameObject granyHead;

	[SerializeField]
	float granyHeadWaitTime;

	[SerializeField]
	float granyDialogueWaitTime;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AnimateLetter() {
		letter.SetActive(true);
		envelope.SetActive(false);
		Invoke("AnimateGranyHead", granyHeadWaitTime);
	}

	private void AnimateGranyHead() {
		granyHead.SetActive(true);
		//Invoke("StartTask", granyDialogueWaitTime);
	}

	private void StartTask() {
		GameObject sceneSwitch = GameObject.FindWithTag("SceneSwitcher");
		SceneSwitcher sceneSwitcher = sceneSwitch.GetComponent<SceneSwitcher>();
		sceneSwitcher.StartNextScene();
	}
}
