using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranyAController : MonoBehaviour {

	//animator for granny B
	Animator _animator;
	GameObject GranyA_Body1;
	Renderer _renderer;

	[SerializeField]
	Material smileMaterial;

	[SerializeField]
	Material scaredMaterial;

	[SerializeField]
	Material sadMaterial;

	void Awake() {
		_animator = GetComponent<Animator>();
		if (_animator==null) // if Animator is missing
			Debug.LogError("Animator component missing from this gameobject");
		GranyA_Body1 = GameObject.Find("GranyA_Body1");
		_renderer = GranyA_Body1.GetComponent<Renderer>();
			if (_renderer==null) // if _renderer is missing
			Debug.LogError("Renderer component missing from this gameobject");
	}

	// Use this for initialization
	void Start () {
		_animator.SetTrigger("Breath");
	}
	
	// Update is called once per frame
/*	void Update () {
		_animator.SetTrigger("Breath");
	}*/

	public void AnimateBreath() {
		_animator.SetTrigger("Breath");
	}

	public void AnimatePanic() {
		_animator.SetTrigger("Panic");
		_animator.SetTrigger("Breath");
	}

	public void Smile() {
		Debug.Log("smile");
		_renderer.material = smileMaterial;
		StartCoroutine(ToSad());
	}

	public void Scared() {
		Debug.Log("scared");
		_renderer.material = scaredMaterial;
		StartCoroutine(ToSad());
	}

	IEnumerator ToSad() {
		yield return new WaitForSeconds(2.0f);
		_renderer.material = sadMaterial;
	}
}
