using UnityEngine;
using UnityEngine.EventSystems;
using static IngerdentFood;

public class Pot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Recipe recipe;

    [SerializeField]
    private float CookingTime;
    [SerializeField]
    private float DeadTime = 9f;
    [SerializeField]
    private float ComplteTime = 7f;
    [SerializeField]
    private float BoilingTime = 2f;

 
    bool broth = false;
    bool greenOnionsEggs = false;

    private SpriteRenderer SelfSprite;

    [SerializeField]
    bool isOrder = false;

    [SerializeField]
    bool isFail;
    [SerializeField]
    MouseHand mouseHand;


    public void Start()
    {
        PotReSet();
    }

    public void Update()
    {
        TestDebug(); 
        Decidebroth();
        UpdateStatus();
        UpdateSprite();
    }
    public void PotReSet()
    {
        recipe = ScriptableObject.CreateInstance<Recipe>();
        broth = false;
        recipe.SetBase(Recipe.Base.none);
        recipe.SetSauce(Recipe.Sauce.none);
        recipe.SetSpecial(Recipe.Special.none);
        recipe.SetStatus(Recipe.Status.Cooking);
        SelfSprite = GetComponent<SpriteRenderer>();
        SelfSprite.color = Color.white;
    }
   
    public void UpdateSprite() 
    {
        var status = recipe.GetStatus;

        switch (status) 
        {
            case Recipe.Status.fail: SelfSprite.color = Color.black; break;
            case Recipe.Status.incomplete: SelfSprite.color = Color.yellow; break;
            case Recipe.Status.complete: SelfSprite.color = Color.red; break;
        }
    }
    public void UpdateStatus() 
    {
       isFail = recipe.GetStatus == Recipe.Status.fail;
        if (CookingTime >= DeadTime && !isFail)
            recipe.SetStatus(Recipe.Status.fail);

        //들어있는 조건에 대한거....만 하면 끝.
        if (DecideStatus() == Recipe.Status.incomplete)
            recipe.SetStatus(Recipe.Status.incomplete);

        if (DecideStatus() == Recipe.Status.complete)
            recipe.SetStatus(Recipe.Status.complete);


        if (CookingTime < BoilingTime && greenOnionsEggs == true)
            isOrder = false;
        else if (CookingTime >= BoilingTime && greenOnionsEggs == true)
            isOrder = true;
    }
    public void Decidebroth()
    {
        if (broth == true && !isFail)
            CookingTime += Time.deltaTime;
    }

    public Recipe.Status DecideStatus() 
    {
        //여기서 조건 검사 
        bool isBase = recipe.GetBase != Recipe.Base.none;
        bool isSauce = recipe.GetSauce != Recipe.Sauce.none;

        // 여기서는 레시피에 다른 것들이 들어있냐? 해당 시간에.
        bool incompleteConditions = CookingTime >= BoilingTime && CookingTime < ComplteTime && !isFail && isBase && isSauce ;
        bool completeConditions = CookingTime >= ComplteTime && CookingTime < DeadTime && !isFail && isOrder && isBase && isSauce;

        if (incompleteConditions)
        {
            recipe.SetStatus(Recipe.Status.incomplete);
            SelfSprite.color = Color.yellow;
            return Recipe.Status.incomplete;
        }
        else
        if (completeConditions)
        {
            //isCompletionTime = true;
            recipe.SetStatus(Recipe.Status.complete);
            SelfSprite.color = Color.red;
            return Recipe.Status.complete;
        }
        else 
        {
            return Recipe.Status.Cooking;
        }
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        var s = recipe.GetStatus;
        // 빈손으로 들었을때. isComplte나 Complte면 Manager에게 전달.
        if (mouseHand.handIngerdentFood == null)
        {
            if (s == Recipe.Status.incomplete || s == Recipe.Status.complete) 
            {
                // 여기서 데이터 값을 매니저에게 보내줘야함.
                Debug.Log("요리 완성. 단계 : " + recipe.GetStatus);
                //그리고 초기화.
                PotReSet();
            }
            return;
        }

        IngerdentFood ingerdentFood = mouseHand.handIngerdentFood.GetComponent<IngerdentFood>();
        InputIngerdentFood(ingerdentFood.ingredientData);
        ingerdentFood.SelfRelease();
    }


    private void InputIngerdentFood(Ingredient collEnum)
    {
        if (isFail)
            return;

        switch (collEnum)
        {
            case Ingredient.Broth: broth = true; break;

            case Ingredient.tteok:
            case Ingredient.noodle: OverlapBase(collEnum); break;

            case Ingredient.soup: break;
            case Ingredient.jjajang: OverSauce(collEnum); break;

            case Ingredient.greenOnionsEggs: greenOnionsEggs = true; break;

            case Ingredient.miwon: recipe.SetSpecial(Recipe.Special.miwon); break;
            case Ingredient.hot: recipe.SetSpecial(Recipe.Special.hot); break;
            case Ingredient.olive: recipe.SetSpecial(Recipe.Special.olive); break;

            default: Debug.Log("새로운 음식은 처리를 못해요."); break;
        }
    }
    private void OverlapBase(Ingredient collEnum)
    {
        if (recipe.GetBase == Recipe.Base.none && collEnum == Ingredient.tteok)
        {
            recipe.SetBase(Recipe.Base.tteok);
        }
        else if (recipe.GetBase == Recipe.Base.none && collEnum == Ingredient.noodle)
        {
            recipe.SetBase(Recipe.Base.noodle);
        }
        else
        if (recipe.GetBase != Recipe.Base.none)
        {
            recipe.SetStatus(Recipe.Status.fail);
        }

    }
    private void OverSauce(Ingredient collEnum)
    {
        if (recipe.GetSauce == Recipe.Sauce.none && collEnum == Ingredient.soup)
        {
            recipe.SetSauce(Recipe.Sauce.soup);
        }
        else if (recipe.GetSauce == Recipe.Sauce.none && collEnum == Ingredient.jjajang)
        {
            recipe.SetSauce(Recipe.Sauce.jjajang);
        }
        else
        if (recipe.GetSauce == Recipe.Sauce.none)
        {
            recipe.SetStatus(Recipe.Status.fail);
        }
    }

    //Debug용 문제 없으면 프로토타입 까지 완료시 지울것.
    public Recipe.Base lookBase;
    public Recipe.Sauce lookSauce;
    public Recipe.Status lookStatus;
    public Recipe.Special lookSpecial;

    public void TestDebug()
    {
        lookBase = recipe.GetBase;
        lookSauce = recipe.GetSauce;
        lookStatus = recipe.GetStatus;
        lookSpecial = recipe.GetSpecial;
    }

}
