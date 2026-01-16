using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static IngerdentFood;

public class Pot : MonoBehaviour, IPointerClickHandler
{
    private Recipe recipe;
    public Recipe GetRecipe() { return recipe; }

    [SerializeField]
    private float CookingTime;
    [SerializeField]
    private float DeadTime =7f;
    [SerializeField]
    private float ComplteTime = 5f;
    [SerializeField]
    private float BoilingTime = 2f;

    [SerializeField]
    bool broth = false;
    bool greenOnionsEggs = false;

    [SerializeField]
    bool isCompletionTime = false;
    [SerializeField]
    bool isOrder= false;

    [SerializeField]
    MouseHand mouseHand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PotReSet();
    }

    // Update is called once per frame
    void Update()
    {
        CookingProcess();
    }
    
    public void PotReSet()
    {
        recipe = ScriptableObject.CreateInstance<Recipe>();
        broth = false;
        recipe.SetBase(Recipe.Base.none);
        recipe.SetSauce(Recipe.Sauce.none);
        recipe.SetSpecial(Recipe.Special.none);
        recipe.SetStatus(Recipe.Status.fail);

    }

    public void CookingProcess() 
    {
        if (broth == true) 
        {
            CookingTime += Time.deltaTime;
        }
        

        if (IsCanUse()) 
        {
            if (isCompletionTime && isOrder)
                recipe.SetStatus(Recipe.Status.complete);

            recipe.SetStatus(Recipe.Status.incomplete);
        }

        if (CookingTime >= DeadTime) 
        {
            recipe.SetStatus(Recipe.Status.fail);
        }

    }
    public bool IsCanUse() 
    {
        bool isBoiling = BoilingTime <= CookingTime;
        bool isBase = recipe.GetBase != Recipe.Base.none;
        bool isSauce = recipe.GetSauce != Recipe.Sauce.none;
        bool isLife = CookingTime <= DeadTime;

        return  greenOnionsEggs && broth && isBase && isSauce && isLife && isBoiling; 
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("클릭 됨");
        if (mouseHand.handIngerdentFood == null)
            return;

        Debug.Log("재료도 있음");
        IngerdentFood ingerdentFood = mouseHand.handIngerdentFood.GetComponent<IngerdentFood>();
        InputIngerdentFood(ingerdentFood.ingredientData);
        ingerdentFood.SelfRelease();
    }

    private void InputIngerdentFood(Ingredient collEnum)
    {
        Debug.Log("InputIngerdentFood 넘김");
        switch (collEnum)
        {
            case Ingredient.Broth: Debug.Log("Broth"); broth = true; break;

            case Ingredient.tteok: Debug.Log("tteok"); recipe.SetBase(Recipe.Base.tteok); break;
            case Ingredient.noodle: Debug.Log("noodle"); recipe.SetBase(Recipe.Base.noodle); break;

            case Ingredient.soup: Debug.Log("soup"); recipe.SetSauce(Recipe.Sauce.soup); break;
            case Ingredient.jjajang: Debug.Log("jjajang"); recipe.SetSauce(Recipe.Sauce.jjajang); break;

            case Ingredient.miwon: recipe.SetSpecial(Recipe.Special.miwon); break;
            case Ingredient.hot: recipe.SetSpecial(Recipe.Special.hot); break;
            case Ingredient.olive: recipe.SetSpecial(Recipe.Special.olive); break;

            default: Debug.Log("새로운 음식은 처리를 못해요."); break;
        }
    }


}
