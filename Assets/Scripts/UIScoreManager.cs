using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreManager : MonoBehaviour {

	GameObject ingredientUIBoard;
	Renderer _renderer;

	[SerializeField]
	string UIBoardName;

	[SerializeField]
	Material[] ingredientMaterials;

	private int count;

	void Awake() {
		ingredientUIBoard = GameObject.Find(UIBoardName);
		if (ingredientUIBoard==null) // if _renderer is missing
			Debug.LogError("ingredientUIBoard component missing from this gameobject");
		_renderer = ingredientUIBoard.GetComponent<Renderer>();
			if (_renderer==null) // if _renderer is missing
			Debug.LogError("Renderer component missing from this gameobject");
		count = 0;
	}

	// Use this for initialization
	void Start () {
		_renderer.material = ingredientMaterials[0];
	}
	
	public void UpdatePepperUI(int number) {
		_renderer.material = ingredientMaterials[number];
	}

	public void UpdateFishUI() {
		_renderer.material = ingredientMaterials[++count];
	}

	public void DestroyFishUI(){
		count = (count/2)*2;
		_renderer.material = ingredientMaterials[count];
	}
}
