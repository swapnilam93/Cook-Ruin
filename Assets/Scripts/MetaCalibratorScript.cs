using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaCalibratorScript : MonoBehaviour {

	MeshRenderer meshRenderer;
	MeshCollider meshCollider;

	[SerializeField]
	GameObject startScreen;

	void Awake() {
		meshRenderer = this.GetComponent<MeshRenderer>();
		meshCollider = this.GetComponent<MeshCollider>();
	}

	public void CalibrateScene() {
		meshRenderer.enabled = false;
		meshCollider.enabled = false;
		startScreen.SetActive(true);
	}
}
