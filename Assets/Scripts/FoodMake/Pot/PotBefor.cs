using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static CooKingDatac;
using static IngerdentFood;
using static Recipe;

public class PotBefor : MonoBehaviour, IPointerClickHandler,IEntity
{
    private Recipe recipe;

    public CooKingDatac cooKData;

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
        Decidebroth();
        UpdateStatus();
    }
    public void PotReSet()
    {
        SelfCollider = gameObject.GetComponent<BoxCollider2D>();
        transform.position = SpwanObject.transform.position;
        cooKData.SetCookingData();

        recipe = ScriptableObject.CreateInstance<Recipe>();
        recipe.SetBase(Recipe.Base.none);
        recipe.SetSauce(Recipe.Sauce.none);
        recipe.SetSpecial(Recipe.Special.none);
        recipe.SetStatus(Recipe.Status.Cooking);
    }

    public void UpdateStatus() 
    {

        isFail = cooKData.GetCurrentStatus() == CooKingDatac.Status.fail;

        // 성공 실패여부만 
        if (isFail)
            recipe.SetStatus(Recipe.Status.fail);

        //끓는 상태
        if (cooKData.CookingTime >= cooKData.BoilingTime) cooKData.broth = CooKingDatac.Broth.boiling;

        // 
        if (cooKData.GetCurrentStatus() == CooKingDatac.Status.incomplete)
            cooKData.status = CooKingDatac.Status.incomplete;

        if (cooKData.GetCurrentStatus() == CooKingDatac.Status.complete)
            cooKData.status = CooKingDatac.Status.complete;

        if (cooKData.CookingTime < cooKData.BoilingTime && cooKData.eggfa == CooKingDatac.Eggfa.greenOnionsEggs)
            isOrder = false;
        else if (cooKData.CookingTime >= cooKData.BoilingTime && cooKData.eggfa == CooKingDatac.Eggfa.greenOnionsEggs)
            isOrder = true;

        if (cooKData.CookingTime >= cooKData.DeadTime)
            cooKData.status = CooKingDatac.Status.fail;
    }
    public void Decidebroth()
    {
        if (cooKData.broth == CooKingDatac.Broth.water && !isFail)
            cooKData.AddTime(Time.deltaTime);
    }

 
    public void OnPointerClick(PointerEventData eventData)
    {
        CooKingDatac.Status s = cooKData.status;
 
        if (mouseHand.Gethand() == null)
        {
            if (s == CooKingDatac.Status.incomplete || s == CooKingDatac.Status.complete) 
            {
                Debug.Log("요리 완성. 단계 : " + recipe.GetStatus);
                GameManager.instance.toogleLoaded();
                GameManager.instance.setRecipe(recipe);
                GameManager.instance.useItem();

                //그리고 초기화.
                
                PotReSet();
            } else if(s == CooKingDatac.Status.fail)
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
            case Ingredient.Broth: cooKData.broth = CooKingDatac.Broth.water; break;

            case Ingredient.tteok:
            case Ingredient.noodle: OverlapBase(collEnum); break;

            case Ingredient.soup: break;
            case Ingredient.jjajang: OverSauce(collEnum); break;

            case Ingredient.greenOnionsEggs: cooKData.eggfa = CooKingDatac.Eggfa.greenOnionsEggs; break;

            case Ingredient.miwon: cooKData.special = CooKingDatac.Special.miwon; break;
            case Ingredient.hot: cooKData.special = CooKingDatac.Special.hot; break;
            case Ingredient.olive: cooKData.special = CooKingDatac.Special.olive; break;

            default: Debug.Log("새로운 음식은 처리를 못해요."); break;
        }
    }
    private void OverlapBase(Ingredient collEnum)
    {
        if (cooKData.foodbase == CooKingDatac.FoodBase.none && collEnum == Ingredient.tteok)
        {
            cooKData.foodbase = CooKingDatac.FoodBase.tteok;
        }
        else if (cooKData.foodbase == CooKingDatac.FoodBase.none && collEnum == Ingredient.noodle)
        {
            cooKData.foodbase = CooKingDatac.FoodBase.noodle;
        }
        else
        if (cooKData.foodbase != CooKingDatac.FoodBase.none)
        {
            cooKData.status = CooKingDatac.Status.fail;
        }

    }
    private void OverSauce(Ingredient collEnum)
    {
        if (cooKData.sauce == CooKingDatac.Sauce.none && collEnum == Ingredient.soup)
        {
            recipe.SetSauce(Recipe.Sauce.soup);
        }
        else if (cooKData.sauce == CooKingDatac.Sauce.none && collEnum == Ingredient.jjajang)
        {
            recipe.SetSauce(Recipe.Sauce.jjajang);
        }
        else
        if (cooKData.sauce != CooKingDatac.Sauce.none)
        {
            recipe.SetStatus(Recipe.Status.fail);
        }
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
