using UnityEngine;
using UnityEngine.EventSystems;

public class FoodStation : MonoBehaviour, IPointerClickHandler
{
    public GameObject IngerdentPrefab;
    [SerializeField]
    MouseHand mouseHand ; 

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mouseHand.handIngerdentFood == null)
        {
            GameObject go = ObjPoolManager.instance.InstantiateFromPool("DebugRiceCakeIngredent");
            go.transform.position = transform.position;
            mouseHand.handIngerdentFood = go;
        }

    }
}
