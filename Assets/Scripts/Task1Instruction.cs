using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1Instruction : MonoBehaviour {

    [SerializeField]
    private GameObject pot;

    [SerializeField]
    private GameObject pepper;

    private bool finish = false;

    private void Start()
    {
        ResetInstruction();
    }

    private void Update()
    {
        if(!finish && pepper == null)
        {
            pepper = GameObject.FindGameObjectsWithTag("PepperTag")[0];
            ResetInstruction();
        }
    }

    public void ResetInstruction()
    {
        pepper.GetComponent<Animator>().SetBool("Glow", true);
        pot.GetComponent<Animator>().SetBool("Glow", false);
    }

    public void NextInstruction()
    {
        pepper.GetComponent<Animator>().SetBool("Glow", false);
        pot.GetComponent<Animator>().SetBool("Glow", true);
    }

    public void StopInstruction()
    {
        pepper.GetComponent<Animator>().SetBool("Exit", true);
        pot.GetComponent<Animator>().SetBool("Exit", true);
        finish = true;

    }

}
