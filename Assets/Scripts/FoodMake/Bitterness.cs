using UnityEngine;
using UnityEngine.EventSystems;

public class Bitterness : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    MouseHand mouseHand;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (mouseHand.Gethand() != null) 
        {
            Debug.Log("dddwww");
            mouseHand.Gethand().GetComponent<IEntity>().SelfRelease(); 
        
        }
    }
}
