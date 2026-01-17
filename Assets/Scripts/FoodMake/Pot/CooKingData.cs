using NUnit.Framework.Interfaces;
using System;
using UnityEngine;

public class CooKingDatac : MonoBehaviour
{
    [SerializeField]
    private float CookingTime;
    [SerializeField]
    private float DeadTime = 9f;
    [SerializeField]
    private float ComplteTime = 7f;
    [SerializeField]
    private float BoilingTime = 2f;

    public MouseHand mouseHand;
    public Status status;
    public FoodBase foodbase;
    public Sauce sauce;
    public Special special;
    public Broth broth;
    public result resultfood;

    public enum Status
    {
        Cooking,
        fail,
        incomplete,
        complete,
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

    public enum Special
    {
        none,
        miwon,
        hot,
        olive
    }

    public enum Broth
    { 
        none,
        water,
        boiling,
    }

    public enum result
    {
        ramen,
        redtteok,
        blackjjajang,
        blacktteok
    }

    public Status GetCurrentStatus()
    {
        if (CookingTime >= DeadTime) return Status.fail;

        bool isBase = foodbase != FoodBase.none;
        bool isSauce = sauce != Sauce.none;

        if (!isBase || !isSauce) return Status.Cooking;

        if (CookingTime >= ComplteTime) return Status.complete;
        if (CookingTime >= BoilingTime) return Status.incomplete;

        return Status.Cooking;
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
