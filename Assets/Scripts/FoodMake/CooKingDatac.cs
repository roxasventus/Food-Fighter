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
    public Special special;

    public FoodBase foodbase;
    public Sauce sauce;
    public Broth broth;
    public Eggfa eggfa;
    public result resultfood;

    public Completion completion;

    public enum Status
    {
        none,
        fail,
        incomplete,
        complete,
    }
    public enum Completion
    {
        none,yesOrder,noOreder
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
        status = Status.none;
        completion = Completion.none;

    }

    public Status GetCurrentStatus()
    {

        bool isBase = foodbase != FoodBase.none;
        bool isSauce = sauce != Sauce.none;

        if (CookingTime >= DeadTime)
            return Status.fail;
        else
        if (isBase && isSauce && CookingTime >= BoilingTime) return Status.incomplete;
        else
        if (isBase && isSauce&& CookingTime >= ComplteTime) return Status.complete;

        return Status.none;
    }

    public result GetResult()
    {
        return result.none;
    }

    public void AddTime(float dt)
    {
        if (broth != Broth.none && status != Status.fail)
            CookingTime += dt;
    }
    /*
     요리 조합 정보와 다른 시스템에 데이터 넘길때 한정된 정보만 넘긴는 메게 변수 제작.
     */
}
