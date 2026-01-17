using UnityEngine;
using UnityEngine.EventSystems;

public class FoodStation : MonoBehaviour, IPointerClickHandler
{
    public GameObject IngerdentPrefab;
    [SerializeField]
    protected MouseHand mouseHand ; 

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mouseHand.Gethand() != null)
            mouseHand.Gethand().GetComponent<IngerdentFood>().SelfRelease();

        GameObject go = ObjPoolManager.instance.InstantiateFromPool(IngerdentPrefab.name.ToString());
        go.transform.position = transform.position;
        mouseHand.Sethand(go);
    }
}
