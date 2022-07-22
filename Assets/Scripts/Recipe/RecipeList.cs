using System.Collections;
using System.Collections.Generic;

public class RecipeList
{
    public List<Recipe> recipes { get; }

    public RecipeList() 
    {
        recipes = new List<Recipe>();
    }

    public void addRecipe(int input, int output, int price, float duration) {
        Recipe recipe = new Recipe(input, output, price, duration);
        recipes.Add(recipe);
    }
}