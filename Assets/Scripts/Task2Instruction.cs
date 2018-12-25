using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2Instruction : MonoBehaviour {

    [SerializeField]
    private GameObject pan;

    [SerializeField]
    private GameObject stove;

    // Use this for initialization
    void Start () {
        ResetInstruction();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ResetInstruction()
    {
        pan.GetComponent<Animator>().SetBool("Glow", true);
        stove.GetComponent<Animator>().SetBool("Glow", false);
    }

    public void NextInstruction()
    {
        pan.GetComponent<Animator>().SetBool("Glow", false);
        stove.GetComponent<Animator>().SetBool("Glow", true);
    }

    public void StopInstruction()
    {
        pan.GetComponent<Animator>().SetBool("Exit", true);
        stove.GetComponent<Animator>().SetBool("Exit", true);
    }

}
