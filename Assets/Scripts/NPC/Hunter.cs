using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : NPC
{
    private HunterUI ui; 

    private bool flag = false;

    void Awake()
    {
        ui = GameObject.Find("Canvas").transform.Find("HunterUI").GetComponent<HunterUI>();
    }

    public override void StartInteract() 
    {
        if(!flag) 
        {
            flag = true;
            UiManager.Instance.OpenUI(ui);
            /*var grl = RecipeManager.Instance.GameRecipeList;
            foreach(var recipe in grl) {
                Debug.Log(recipe.Input + ", " + recipe.Output + ", " + recipe.Duration);
            }*/
        }
    }

    public override void EndInteract() 
    {
        if(flag)
        {
            flag = false;
            UiManager.Instance.CloseUI(ui);
        }
    }
}