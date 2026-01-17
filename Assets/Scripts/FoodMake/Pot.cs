using UnityEngine;
using UnityEngine.EventSystems;
using static IngerdentFood;

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

    bool broth = false;
    bool greenOnionsEggs = false;

    private SpriteRenderer SelfSprite;
    private BoxCollider2D SelfCollider;

    bool isOrder = false;
    bool isFail;

    [SerializeField]
    MouseHand mouseHand;

    public GameObject SpwanObject;

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
        SelfSprite = GetComponent<SpriteRenderer>();
        SelfSprite.color = Color.white;
        CookingTime = 0f;
        SelfCollider.enabled = true;
    }

    public void UpdateAnimation() 
    {
        var status = recipe.GetStatus;

        if (broth == true && !isFail)
            //��ǰ �ִϸ��̼� 


            //�ִϸ��̼� ���� �κ�.
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
        //���⼭ ���� �˻� 
        bool isBase = recipe.GetBase != Recipe.Base.none;
        bool isSauce = recipe.GetSauce != Recipe.Sauce.none;

        // ���⼭�� �����ǿ� �ٸ� �͵��� ����ֳ�? �ش� �ð���.
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
                // ���⼭ ������ ���� �Ŵ������� ���������.
                Debug.Log("�丮 �ϼ�. �ܰ� : " + recipe.GetStatus);
                GameManager.instance.toogleLoaded();
                GameManager.instance.setRecipe(recipe);
                GameManager.instance.useItem();
                //�׸��� �ʱ�ȭ.
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

            default: Debug.Log("���ο� ������ ó���� ���ؿ�."); break;
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

    //Debug�� ���� ������ ������Ÿ�Ա��� �Ϸ�� �����.
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
        
        PotReSet();
    }

    public void SelfRelease()
    {
        mouseHand.Sethand(null);
        PotReSet();
    }
}
