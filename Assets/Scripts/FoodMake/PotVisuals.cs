using System;
using System.Collections.Generic;
using UnityEngine;
using static CooKingDatac;

public class PotVisuals : MonoBehaviour
{

    public FoodBase foodbase;
    public Sauce sauce;
    public Broth broth;
    public Eggfa eggfa;
    public result resultfood;

    [SerializeField] private Sprite[] PotStatusSprites;

    [SerializeField] private GameObject[] borthSprites;
    [SerializeField] private GameObject[] foodbaseSprites;
    [SerializeField] private GameObject[] SauceSprites;
    [SerializeField] private GameObject[] eggfaSprites;

    [SerializeField] private GameObject[] resultSprite;
    [SerializeField] private SpriteRenderer water;

    public void Update()
    {
        //나중에 변화할때만 받도록.

    }
    public void ChangeFoodImage(CooKingDatac cooKData)
    {
        if(cooKData.status == CooKingDatac.Status.fail)
        {
            ResetVisuals ();
            return;
        }

        if(cooKData.status == CooKingDatac.Status.complete)
        {
            ResetVisuals ();

            //완성된 음식
            if(cooKData.foodbase == FoodBase.noodle && cooKData.sauce == Sauce.soup)
            {
                resultSprite[0].SetActive(true); //완성된 국수
            }
            else if(cooKData.foodbase == FoodBase.noodle && cooKData.sauce == Sauce.jjajang)
            {
                resultSprite[1].SetActive(true); //완성된 짜장면
            }
            else if (cooKData.foodbase == FoodBase.tteok && cooKData.sauce == Sauce.soup)
            {
                resultSprite[2].SetActive(true); //완성된 떡볶이
            }
            else if (cooKData.foodbase == FoodBase.tteok && cooKData.sauce == Sauce.jjajang)
            {
                resultSprite[3].SetActive(true); //완성된 짜장떡볶이
            }
            eggfaSprites[0].SetActive(true);


            return;
        }


         // 물 or 끓는물
        if(cooKData.broth == Broth.water)
        {
            borthSprites[0].SetActive(true);
            borthSprites[1].SetActive(false);
        }
        else if(cooKData.broth == Broth.boiling)
        {
            borthSprites[0].SetActive(false);
            borthSprites[1].SetActive(true);
        }
        else
        {
            borthSprites[0].SetActive(false);
            borthSprites[1].SetActive(false);
        }

        

        // 면 or 떡
        if(cooKData.foodbase == FoodBase.noodle)
        {
            foodbaseSprites[0].SetActive(true);
            foodbaseSprites[1].SetActive(false);
        }
        else if(cooKData.foodbase == FoodBase.tteok)
        {
            foodbaseSprites[0].SetActive(false);
            foodbaseSprites[1].SetActive(true);
        }
        else
        {
            foodbaseSprites[0].SetActive(false);
            foodbaseSprites[1].SetActive(false);
        }

        if(cooKData.broth == Broth.water || cooKData.broth == Broth.boiling) // 물 + 소스
        {
            SauceSprites[0].SetActive(false);
            SauceSprites[1].SetActive(false);
            water = borthSprites[0].gameObject.activeSelf ? borthSprites[0].GetComponent<SpriteRenderer>() : borthSprites[1].GetComponent<SpriteRenderer>();
            if (cooKData.sauce == Sauce.none)
            {
                water.color = new Color(0.7f, 0.7f, 1f, 1f);
            }
            else if (cooKData.sauce == Sauce.soup)
            {
                water.color = new Color(1f, 0.5f, 0.5f, 1f);
            }
            else if (cooKData.sauce == Sauce.jjajang)
            {
                water.color = new Color(0.5f, 0.2f, 0.2f, 1f);
            }
        }
        else // 물X 소스 봉지
        {
            if (cooKData.sauce == Sauce.soup)
            {
                SauceSprites[0].SetActive(true);
                SauceSprites[1].SetActive(false);
            }
            else if (cooKData.sauce == Sauce.jjajang)
            {
                SauceSprites[0].SetActive(false);
                SauceSprites[1].SetActive(true);
            }
            else
            {
                SauceSprites[0].SetActive(false);
                SauceSprites[1].SetActive(false);
            }
        }
        
        // 계란파
        if(cooKData.eggfa == Eggfa.greenOnionsEggs)
        {
            eggfaSprites[0].SetActive(true);
        }
        else
        {
            eggfaSprites[0].SetActive(false);
        }

    }
    /*
    CookData 변화에 따른 내부 데이터 변동을 받아서 sprite 표현.
    PotStatusSprites의 0번 인덱스는 탄것
    PotStatusSprites의 1번 인덱스는 안탄것
    */
    public void ChangeStatusSprite(int index)
    {
        SpriteRenderer Pot = gameObject.GetComponent<SpriteRenderer>();
        Pot.sprite = PotStatusSprites[index];
    }


    public void ResetVisuals()
    {
        foreach (var item in borthSprites)
        {
            item.SetActive(false);
        }
        foreach (var item in foodbaseSprites)
        {
            item.SetActive(false);
        }
        foreach (var item in SauceSprites)
        {
            item.SetActive(false);
        }
        foreach (var item in eggfaSprites)
        {
            item.SetActive(false);
        }
        foreach (var item in resultSprite)
        {
            item.SetActive(false);
        }
    }


}
