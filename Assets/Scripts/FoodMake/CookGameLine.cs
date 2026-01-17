using UnityEngine;
using UnityEngine.EventSystems;

public class CookGameLine : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    MouseHand mouseHand;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hi");
        if (mouseHand.Gethand() != null)
            mouseHand.Gethand().GetComponent<IEntity>().SelfRelease();
    }
}
