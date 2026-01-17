using NUnit.Framework.Interfaces;
using System;
using UnityEngine;

public class CooKingDatac : MonoBehaviour
{
    [SerializeField]
    public float CookingTime;
    [SerializeField]
    public float DeadTime = 9f;
    [SerializeField]
    public float ComplteTime = 7f;
    [SerializeField]
    public float BoilingTime = 2f;

    public MouseHand mouseHand;

    public Status status;
    public FoodBase foodbase;
    public Sauce sauce;
    public Special special;
    public Broth broth;
    public Eggfa eggfa;
    public result resultfood;
    public Completion completion;

    public enum Status
    {
        None,
        fail,
        incomplete,
        complete,
    }
    public enum Completion
    {
       Cooking,End
    }
    public enum FoodBase
    {
        none,
        noodle,
        tteok
    }

    public enum Sauce
    {
        none,
        soup,
        jjajang
    }

    public enum Broth
    {
        none,
        water,
        boiling,
    }

    public enum Eggfa
    {
        none,
        greenOnionsEggs
    }

    public enum Special
    {
        none,
        miwon,
        hot,
        olive
    }

    public enum result
    {
        none,
        ramen,
        redtteok,
        blackjjajang,
        blacktteok
    }

    public void SetCookingData()
    {
        CookingTime = 0f;
        resultfood = result.none;
        eggfa = Eggfa.none;
        broth = Broth.none;
        special = Special.none;
        sauce = Sauce.none;
        foodbase = FoodBase.none;
        status = Status.None;
        completion = Completion.NotStart;

    }

    public Status GetCurrentStatus()
    {
        if (CookingTime >= DeadTime) return Status.fail;

        bool isBase = foodbase != FoodBase.none;
        bool isSauce = sauce != Sauce.none;

        if (!isBase || !isSauce) return Status.None;

        if (CookingTime >= ComplteTime) return Status.complete;
        if (CookingTime >= BoilingTime) return Status.incomplete;


        return Status.fail;
    }

    public result GetResult() 
    {
       return result.none;
    }

    public void AddTime(float dt)
    {
        if (broth == Broth.water && status != Status.fail)
            CookingTime += dt;
    }
    /*
     요리 조합 정보와 다른 시스템에 데이터 넘길때 한정된 정보만 넘긴는 메게 변수 제작.
     */
}
