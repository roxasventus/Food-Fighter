using UnityEngine;
using UnityEngine.EventSystems;

public class FoodStation : MonoBehaviour, IPointerClickHandler
{
    public GameObject IngerdentPrefab;
    [SerializeField]
    protected MouseHand mouseHand ;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (mouseHand.Gethand() != null)
            mouseHand.Gethand().GetComponent<IEntity>().SelfRelease();

        GameObject go = ObjPoolManager.instance.InstantiateFromPool(IngerdentPrefab.name.ToString());
        go.transform.position = transform.position;
        mouseHand.Sethand(go);
    }
}
