using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IngredientManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ingredient;

    [SerializeField]
    private string ingredientTag;

    [SerializeField]
    private string ingredientFinalTag;

    [SerializeField]
    private Transform task1;

    private bool canGenerate = true;


    private void OnTriggerExit(Collider collision){
        if(canGenerate && collision.CompareTag(ingredientTag)){       
            Invoke("Instantiateprocess", 2.0f);
            collision.tag = ingredientFinalTag;
        }
    }

    public void Instantiateprocess(){       
        GameObject.Instantiate(ingredient,task1);
    }

}

