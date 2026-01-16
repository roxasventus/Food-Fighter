using UnityEngine;
using UnityEngine.EventSystems;

public class FoodStation : MonoBehaviour, IPointerClickHandler
{
    public GameObject IngerdentPrefab;
    [SerializeField]
    MouseHand mouseHand ; 

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mouseHand.handIngerdentFood != null)
            mouseHand.handIngerdentFood.GetComponent<IngerdentFood>().SelfRelease();

        GameObject go = ObjPoolManager.instance.InstantiateFromPool(IngerdentPrefab.name.ToString());
        go.transform.position = transform.position;
        mouseHand.handIngerdentFood = go;
    }
}
