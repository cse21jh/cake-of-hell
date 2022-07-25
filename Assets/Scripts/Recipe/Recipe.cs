using System.Collections;
using System.Collections.Generic;

public class Recipe
{
    public int Input { get; private set; }
    public int Output { get; private set;  }
    public float Price { get; private set; }
    public float Duration { get; private set; }

    public Recipe(int input, int output, float price, float duration) {
        Input = input;
        Output = output;
        Price = price;
        Duration = duration;
    }
}