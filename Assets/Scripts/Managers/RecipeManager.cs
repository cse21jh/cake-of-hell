using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class RecipeManager : Singleton<RecipeManager>
{
    private RecipeList _GameRecipeList;

    public ReadOnlyCollection<Recipe> GameRecipeList 
    {
        get { return _GameRecipeList.recipes.AsReadOnly(); }
    }

    void Awake() 
    {
        _GameRecipeList = new RecipeList();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        AddGameRecipes();
    }

    void Update()
    {
        
    }

    void AddGameRecipes() {
        //Base
        _GameRecipeList.addRecipe("진흙", "저주받은 흙", ItemLevel.C);
        _GameRecipeList.addRecipe("가죽", "광택이 나는 레더", ItemLevel.A);
        _GameRecipeList.addRecipe("거미 뼈", "뼈 반죽", ItemLevel.SS);
        _GameRecipeList.addRecipe("독버섯", "버섯 빵", ItemLevel.B);
        _GameRecipeList.addRecipe("악마의 심장", "붉은 심장", ItemLevel.S);

        //Icing
        _GameRecipeList.addRecipe("바람 한 병", "회오리", ItemLevel.B);
        _GameRecipeList.addRecipe("비늘", "무지갯빛 가루", ItemLevel.SS);
        _GameRecipeList.addRecipe("핏방울", "피냄새 크림", ItemLevel.S);
        _GameRecipeList.addRecipe("목소리", "목소리 크림", ItemLevel.B);
        _GameRecipeList.addRecipe("독", "독 크림", ItemLevel.A);
        _GameRecipeList.addRecipe("독버섯", "독버섯 크림", ItemLevel.B);

        //Topping
        _GameRecipeList.addRecipe("모래", "유리 파편", ItemLevel.A);
        _GameRecipeList.addRecipe("뿔 조각", "레드 콘", ItemLevel.A);
        _GameRecipeList.addRecipe("거미줄", "썩은 거미줄", ItemLevel.SS);
        _GameRecipeList.addRecipe("인어 비늘", "비늘 가루", ItemLevel.C);
        _GameRecipeList.addRecipe("이빨", "이빨 초콜릿", ItemLevel.A);
        _GameRecipeList.addRecipe("악한 영혼", "악한 영혼의 가루", ItemLevel.S);
    }
}