using UnityEngine;
using UnityEngine.EventSystems;

public class CookGameLine : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    MouseHand mouseHand;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mouseHand.Gethand() != null)
            mouseHand.Gethand().GetComponent<IEntity>().SelfRelease();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseHand.Gethand() != null)
            mouseHand.Gethand().GetComponent<IEntity>().SelfRelease();
    }
}
