using UnityEngine;
using UnityEngine.EventSystems;

public class FoodStation : MonoBehaviour, IPointerClickHandler
{
    public GameObject IngerdentPrefab;
    [SerializeField]
    protected MouseHand mouseHand ;
    [SerializeField]
    protected GameObject go;
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Shake");
        if (mouseHand.Gethand() != null)
            mouseHand.Gethand().GetComponent<IEntity>().SelfRelease();

        go = ObjPoolManager.instance.InstantiateFromPool(IngerdentPrefab.name.ToString());
        go.transform.position = transform.position;
        mouseHand.Sethand(go);
        //| SOUND
        SoundManager.instance.PlaySound("GrabFoodSupplies", 1f);
    }
}
