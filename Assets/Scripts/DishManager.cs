using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour {

    [SerializeField]
    private string pepperTag;

    public int pepperCount = 0;

    [SerializeField]
    public AudioSource[] bgm;

    [SerializeField]
    GameObject successBox;
    [SerializeField]
    GameObject pepperUI;

    UIScoreManager UIScoreManager;

    void Awake() {
        UIScoreManager = GameObject.Find("pepperUIBoard").GetComponent<UIScoreManager>();
        if (UIScoreManager==null) // if UIScoreManager is missing
			Debug.LogError("UIScoreManager component missing from this gameobject");
    }

    public void IngredientIn(string ingredientTag)
    {
        if (ingredientTag == pepperTag){
            pepperCount++;
            UpdateBackgroundSound();
            Debug.Log(pepperCount);
        }
        //if (pepperCount == 4)
            //Debug.Log("You Win!");

    }

    public void IngredientOut(string ingredientTag)
    {
        if (ingredientTag == pepperTag){
            pepperCount--;
            UpdateBackgroundSound();
            Debug.Log(pepperCount);
        }
    }

    //update the sounds to be played based on the number of peppers in the pot
    public void UpdateBackgroundSound() {
        switch(pepperCount) {
            case 0:
                bgm[1].mute = true;
                bgm[2].mute = true;
                bgm[3].mute = true;
                bgm[4].mute = true;
                bgm[5].mute = true;
                break;
            case 1:
                bgm[1].mute = false;
                bgm[2].mute = true;
                bgm[3].mute = true;
                bgm[4].mute = true;
                bgm[5].mute = true;
                break;
            case 2:
                bgm[1].mute = false;
                bgm[2].mute = false;
                bgm[3].mute = true;
                bgm[4].mute = true;
                bgm[5].mute = true;
                break;
            case 3:
                bgm[1].mute = false;
                bgm[2].mute = false;
                bgm[3].mute = false;
                bgm[4].mute = true;
                bgm[5].mute = true;
                break;
            case 4:
                bgm[1].mute = false;
                bgm[2].mute = false;
                bgm[3].mute = false;
                bgm[4].mute = false;
                bgm[5].mute = true;
                break;
            case 5:
                bgm[1].mute = false;
                bgm[2].mute = false;
                bgm[3].mute = false;
                bgm[4].mute = false;
                bgm[5].mute = false;
                break;
        }
        UIScoreManager.UpdatePepperUI(pepperCount);
    }

    public bool GetWinStatus()
    {
        return pepperCount == 5;
    }

    public void TriggerWin()
    {        
        Debug.Log("You Win");
        //Time.timeScale = 0;
        StartCoroutine(DisappearUI());
    }

    // public void SwitchToNextTask() {
	// 	GameObject sceneSwitch = GameObject.FindWithTag("SceneSwitcher");
	// 	SceneSwitcher sceneSwitcher = sceneSwitch.GetComponent<SceneSwitcher>();
	// 	sceneSwitcher.StartNextScene();
	// }

    IEnumerator DisappearUI() {
        GameObject.Find("GranyB").GetComponent<Patrol>().SetMoveSpeed(0f);
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("pepperUIBoard").GetComponent<Animator>().SetTrigger("Disappear");
        yield return new WaitForSeconds(.5f);
        pepperUI.SetActive(false);
        successBox.SetActive(true);
    }

    /*public void SubtractPepperCount() {
        pepperCount--;
        UpdateBackgroundSound();
    }*/
}
