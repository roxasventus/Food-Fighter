using UnityEngine;

public class PotVisuals : MonoBehaviour
{
    CooKingDatac cooKData;

    [SerializeField] private Sprite[] PotStatusSprites;

    /*
    CookData 변화에 따른 내부 데이터 변동을 받아서 sprite 표현.
    PotStatusSprites의 0번 인덱스는 탄것
    PotStatusSprites의 1번 인덱스는 안탄것
    */
    public void ChangeStatusSprite(int index) {
        SpriteRenderer Pot = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Pot.sprite = PotStatusSprites[index];
    }
}
