using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static CooKingDatac;
using static IngerdentFood;
using static Recipe;

public class Pot : MonoBehaviour, IPointerClickHandler, IEntity
{
    private Recipe recipe;

    public CooKingDatac cooKData;

    private BoxCollider2D SelfCollider;

    [SerializeField]
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
        gameObject.GetComponent<PotVisuals>().ChangeStatusSprite(1);
        SelfCollider.enabled = true;
        recipe = ScriptableObject.CreateInstance<Recipe>();
        recipe.SetBase(Recipe.Base.none);
        recipe.SetSauce(Recipe.Sauce.none);
        recipe.SetSpecial(Recipe.Special.none);
        recipe.SetStatus(Recipe.Status.Cooking);
    }

    public void UpdateStatus()
    {
        // 값 조건 확인.

        isFail = cooKData.GetCurrentStatus() == CooKingDatac.Status.fail;

        // 성공 실패여부만 
        if (isFail)
        {
            recipe.SetStatus(Recipe.Status.fail);
            gameObject.GetComponent<PotVisuals>().ChangeStatusSprite(0);
        }

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
    }
    public void Decidebroth()
    {
        if (cooKData.broth != CooKingDatac.Broth.none && !isFail)
            cooKData.AddTime(+Time.deltaTime);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        CooKingDatac.Status s = cooKData.status;

        if (mouseHand.Gethand() == null)
        {
            if (s == CooKingDatac.Status.incomplete || s == CooKingDatac.Status.complete)
            {
                Debug.Log("요리 완성. 단계 : " + recipe.GetStatus);

                if (cooKData.foodbase == FoodBase.noodle) { recipe.SetBase(Recipe.Base.noodle); }
                else
                    if (cooKData.foodbase == FoodBase.tteok) { recipe.SetBase(Recipe.Base.tteok); }

                if (cooKData.status == CooKingDatac.Status.incomplete) { recipe.SetStatus(Recipe.Status.incomplete); }
                else
                    if (cooKData.status == CooKingDatac.Status.complete) { recipe.SetStatus(Recipe.Status.complete); }

                if (cooKData.sauce == CooKingDatac.Sauce.soup) { recipe.SetSauce(Recipe.Sauce.soup); }
                else
                    if (cooKData.sauce == CooKingDatac.Sauce.jjajang) { recipe.SetSauce(Recipe.Sauce.jjajang); }

                if (cooKData.special == CooKingDatac.Special.miwon) { recipe.SetSpecial(Recipe.Special.miwon); }
                else
                    if (cooKData.special == CooKingDatac.Special.olive) { recipe.SetSpecial(Recipe.Special.olive); }
                    else
                        if (cooKData.special == CooKingDatac.Special.hot) { recipe.SetSpecial(Recipe.Special.hot); }

                GameManager.instance.toogleLoaded();
                GameManager.instance.setRecipe(recipe);
                GameManager.instance.useItem();

                //그리고 초기화.

                PotReSet();
            }
            else if (s == CooKingDatac.Status.fail)
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
        {
            Debug.Log("ddww");
            gameObject.GetComponent<PotVisuals>().ChangeStatusSprite(0);
            return;
        }

        switch (collEnum)
        {
            case Ingredient.Broth: cooKData.broth = CooKingDatac.Broth.water; break;

            case Ingredient.tteok:
            case Ingredient.noodle: OverlapBase(collEnum); break;

            case Ingredient.soup:
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
                gameObject.GetComponent<PotVisuals>().ChangeStatusSprite(0);
            }

    }
    private void OverSauce(Ingredient collEnum)
    {
        if (cooKData.sauce == CooKingDatac.Sauce.none && collEnum == Ingredient.soup)
        {
            cooKData.sauce = CooKingDatac.Sauce.soup;
            //recipe.SetSauce(Recipe.Sauce.soup);
        }
        else if (cooKData.sauce == CooKingDatac.Sauce.none && collEnum == Ingredient.jjajang)
        {
            cooKData.sauce = CooKingDatac.Sauce.jjajang;
            //recipe.SetSauce(Recipe.Sauce.jjajang);
        }
        else
        if (cooKData.sauce != CooKingDatac.Sauce.none)
        {
          cooKData.status = CooKingDatac.Status.fail;
                //recipe.SetStatus(Recipe.Status.fail);
          gameObject.GetComponent<PotVisuals>().ChangeStatusSprite(0);
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
