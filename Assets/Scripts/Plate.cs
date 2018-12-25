using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour {

    [SerializeField]
    private Transform[] Pos;

    private int fishCnt = 0;

    [SerializeField]
    public AudioSource[] bgm;

    [SerializeField]
    GameObject successBox;
    [SerializeField]
    GameObject fishUI;

    public Vector3 InPlate()
    {
        fishCnt++;
        UpdateBackgroundSound();
        Debug.Log(fishCnt);
        if (fishCnt == 3)
        {
            Debug.Log("You Win!");
            StartCoroutine(DisappearUI());
        }
        return Pos[fishCnt-1].position;        
    }

    IEnumerator DisappearUI()
    {
        GameObject.Find("GranyB").GetComponent<Patrol>().SetMoveSpeed(0f);
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("fishUIBoard").GetComponent<Animator>().SetTrigger("Disappear");
        yield return new WaitForSeconds(.5f);
        fishUI.SetActive(false);
        successBox.SetActive(true);
    }


    public void UpdateBackgroundSound()
    {
        switch (fishCnt)
        {
            case 0:
                bgm[1].mute = true;
                bgm[2].mute = true;
                bgm[3].mute = true;
                bgm[4].mute = true;
                bgm[5].mute = true;
                break;
            case 1:
                bgm[1].mute = false;
                bgm[2].mute = false;
                bgm[3].mute = true;
                bgm[4].mute = true;
                bgm[5].mute = true;
                break;
            case 2:
                bgm[1].mute = false;
                bgm[2].mute = false;
                bgm[3].mute = false;
                bgm[4].mute = false;
                bgm[5].mute = true;
                break;
            case 3:
                bgm[1].mute = false;
                bgm[2].mute = false;
                bgm[3].mute = false;
                bgm[4].mute = false;
                bgm[5].mute = false;
                break;
        }
    }

}
