using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static IngerdentFood;
using static Recipe;

public class Pot : MonoBehaviour, IPointerClickHandler,IEntity
{
    private Recipe recipe;

    [SerializeField]
    private float CookingTime;
    [SerializeField]
    private float DeadTime = 9f;
    [SerializeField]
    private float ComplteTime = 7f;
    [SerializeField]
    private float BoilingTime = 2f;

    [SerializeField]
    bool broth = false;
    bool greenOnionsEggs = false;

    private BoxCollider2D SelfCollider;

    bool isOrder = false;
    bool isFail;

    [SerializeField]
    MouseHand mouseHand;

    public Animator SpriteAnimator;

    public GameObject SpwanObject;
    //
    [SerializeField]
    private GameObject WaterSprite,boilingSprite;
    [SerializeField]
    private GameObject Soupsprite, jjajangsprite;
    [SerializeField]
    private GameObject noodleSprite,tteokSprite;
    [SerializeField]
    private GameObject greenOnionsEggsSprite;
    public void Start()
    {
        PotReSet();
    }

    public void Update()
    {
        TestDebug(); 
        Decidebroth();
        UpdateStatus();
        UpdateAnimation();
    }
    public void PotReSet()
    {
        SelfCollider = gameObject.GetComponent<BoxCollider2D>();
        transform.position = SpwanObject.transform.position;
        recipe = ScriptableObject.CreateInstance<Recipe>();
        broth = false;
        recipe.SetBase(Recipe.Base.none);
        recipe.SetSauce(Recipe.Sauce.none);
        recipe.SetSpecial(Recipe.Special.none);
        recipe.SetStatus(Recipe.Status.Cooking);
        SpriteAnimator = GetComponentInChildren<Animator>();
        if (SpriteAnimator != null)
            SpriteAnimator.SetTrigger("Summon");
        CookingTime = 0f;
        SelfCollider.enabled = true;

        SetChildSprite();
    }
    public void SetChildSprite()
    {
        WaterSprite.SetActive(false);
        boilingSprite.SetActive(false);
        Soupsprite.SetActive(false);
        jjajangsprite.SetActive(false);
        noodleSprite.SetActive(false);
        tteokSprite.SetActive(false);
        greenOnionsEggsSprite.SetActive(false);
        /*
          WaterSprite = transform.Find("Water").gameObject;
         boilingSprite = transform.Find("boiling").gameObject;
         Soupsprite = transform.Find("Soup").gameObject;
         jjajangsprite = transform.Find("jjajang").gameObject;
         noodleSprite = transform.Find("noodle").gameObject;
         tteokSprite = transform.Find("tteok").gameObject;
         greenOnionsEggsSprite = transform.Find("greenOnionsEggs").gameObject;
         */

    }
    public void UpdateAnimation()
    { 
        bool isBorring = (CookingTime >= BoilingTime);
        Recipe.Status status = recipe.GetStatus;

        if (broth == true && isBorring !=true)
        {
            WaterSprite.SetActive(true);
        }
        else if (broth != true )
        {
            WaterSprite.SetActive(false);
        }
        //애니메이션 적용 부분.//거품 스프라이트 .
        if (isBorring == true)
        {
            SpriteAnimator.SetBool("boiling", true);
            boilingSprite.SetActive(true);
            WaterSprite.SetActive(false);

        }
        else if (isBorring == false) 
        {
            SpriteAnimator.SetBool("boiling", false);
        }
 
        switch (recipe.GetBase)
        {
            case Base.none: noodleSprite.SetActive(false); tteokSprite.SetActive(false); break;
            case Base.tteok: tteokSprite.SetActive(true); noodleSprite.SetActive(false); break;
            case Base.noodle: noodleSprite.SetActive(true); tteokSprite.SetActive(false); break;
        }
        switch (recipe.GetSauce)
        {
            case Sauce.none: noodleSprite.SetActive(false); tteokSprite.SetActive(false); break;
            case Sauce.soup: tteokSprite.SetActive(true); noodleSprite.SetActive(false); break;
            case Sauce.jjajang: noodleSprite.SetActive(true); tteokSprite.SetActive(false); break;

        }
        if (greenOnionsEggs == true)
            greenOnionsEggsSprite.SetActive(false);
        else greenOnionsEggsSprite.SetActive(false);

        switch (status) 
        {
            case Recipe.Status.fail: break;
            case Recipe.Status.incomplete: break;
            case Recipe.Status.complete:  break;
        }
    }
    public void UpdateStatus() 
    {
       isFail = recipe.GetStatus == Recipe.Status.fail;
        if (CookingTime >= DeadTime && !isFail)
            recipe.SetStatus(Recipe.Status.fail);

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
        bool isBase = recipe.GetBase != Recipe.Base.none;
        bool isSauce = recipe.GetSauce != Recipe.Sauce.none;

        bool incompleteConditions = CookingTime >= BoilingTime && CookingTime < ComplteTime && !isFail && isBase && isSauce ;
        bool completeConditions = CookingTime >= ComplteTime && CookingTime < DeadTime && !isFail && isOrder && isBase && isSauce;

        if (incompleteConditions)
        {
            recipe.SetStatus(Recipe.Status.incomplete);
            return Recipe.Status.incomplete;
        }
        else
        if (completeConditions)
        {
            //isCompletionTime = true;
            recipe.SetStatus(Recipe.Status.complete);
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
 
        if (mouseHand.Gethand() == null)
        {
            if (s == Recipe.Status.incomplete || s == Recipe.Status.complete) 
            {
                Debug.Log("요리 완성. 단계 : " + recipe.GetStatus);
                GameManager.instance.toogleLoaded();
                GameManager.instance.setRecipe(recipe);
                GameManager.instance.useItem();
                //그리고 초기화.
                
                PotReSet();
            } else if(s == Recipe.Status.fail)
            {
                mouseHand.Sethand(gameObject);
                SelfCollider.enabled = false;
            }
            return;
        }

        
        IngerdentFood ingerdentFood = mouseHand.Gethand().GetComponent<IngerdentFood>();
        InputIngerdentFood(ingerdentFood.ingredientData);
        mouseHand.Sethand(null);
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
        if (recipe.GetSauce != Recipe.Sauce.none)
        {
            recipe.SetStatus(Recipe.Status.fail);
        }
    }

    //Debug용 문제 없으면 프로토타입까지 완료시 지울것.
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

    public void EntityReset()
    {
    }

    public void SelfRelease()
    {
        mouseHand.Sethand(null);
        PotReSet();
    }
}
