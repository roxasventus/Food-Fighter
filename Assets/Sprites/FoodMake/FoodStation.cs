using UnityEngine;
using UnityEngine.EventSystems;

public class FoodStation : MonoBehaviour, IPointerClickHandler
{
    public GameObject IngerdentPrefab;
    [SerializeField]
    MouseHand mouseHand ; 

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("IPointerClickHandler Å¬¸¯µÊ");
       GameObject go =  Instantiate(IngerdentPrefab, transform.position, Quaternion.identity);
        mouseHand.handIngerdentFood = go.GetComponent<IngerdentFood>();
    }
}
