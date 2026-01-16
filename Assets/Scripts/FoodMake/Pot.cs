using System;
using UnityEngine;
using UnityEngine.Rendering;
using static IngerdentFood;

public class Pot : MonoBehaviour
{
    Recipe recipe = new Recipe();
    
    [SerializeField]
    private float CookingTime;
    [SerializeField]
    private float DeadTime =5f;
    [SerializeField]
    private float BoilingTime = 2f;

    [SerializeField]
    bool broth = false;
    bool greenOnionsEggs = false;

    [SerializeField]
    bool isCompletionTime = false;
    [SerializeField]
    bool isOrder= false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReSet();
    }

    // Update is called once per frame
    void Update()
    {
        CookingProcess();
    }
    
    public void ReSet()
    {
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enum collEnum = collision.GetComponent<IngerdentFood>().ingredientData;
        if (collEnum != null) 
        {

        }
      

    }

}
