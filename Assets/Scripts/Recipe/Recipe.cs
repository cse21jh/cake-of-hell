using System.Collections;
using System.Collections.Generic;

public class Recipe
{
    private float[] durationsByLevel = new float[5] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f };

    public string Input { get; set; }
    public string Output { get; set; }
    public float Duration { get; set; }

    public Recipe(string input, string output, ItemLevel itemLevel) {
        this.Input = input;
        this.Output = output;
        this.Duration = durationsByLevel[(int)itemLevel];
    }
}