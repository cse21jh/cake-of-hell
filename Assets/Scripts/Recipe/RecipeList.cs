using System.Collections;
using System.Collections.Generic;

public class RecipeList
{
    public List<Recipe> recipes { get; }

    public RecipeList() 
    {
        recipes = new List<Recipe>();
    }

    public void addRecipe(string input, string output, ItemLevel itemLevel) {
        Recipe recipe = new Recipe(input, output, itemLevel);
        recipes.Add(recipe);
    }
}