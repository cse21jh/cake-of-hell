using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : NPC
{
    private bool flag = false;

    public override void StartInteract() 
    {
        if(!flag) 
        {
            flag = true;
            var grl = RecipeManager.Instance.GameRecipeList;
            foreach(var recipe in grl) {
                Debug.Log(recipe.Input + ", " + recipe.Output + ", " + recipe.Duration);
            }
        }
    }

    public override void EndInteract() 
    {
        if(flag)
        {
            flag = false;
        }
    }
}