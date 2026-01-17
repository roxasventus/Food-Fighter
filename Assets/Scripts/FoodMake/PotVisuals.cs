using System.Collections.Generic;
using UnityEngine;
using static CooKingDatac;

public class PotVisuals : MonoBehaviour
{
    [SerializeField]
    CooKingDatac cooKData;

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

    public void Update()
    {
        //나중에 변화할때만 받도록.

    }
    public void ChangeFoodImage()
    {
       ;
        if (cooKData.foodbase == FoodBase.tteok) { }
        else if (cooKData.foodbase == FoodBase.noodle) { }
        else {  }

        if (cooKData.sauce == Sauce.jjajang) { }
        else if (cooKData.sauce == Sauce.soup) { }

        if (cooKData.broth == Broth.water) { }
        else if (cooKData.broth == Broth.boiling) { }

        if (cooKData.eggfa == Eggfa.greenOnionsEggs) { }
        else {  }

        if (cooKData.resultfood == resultfood) 
        {
        
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


}
