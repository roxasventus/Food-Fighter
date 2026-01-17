using UnityEngine;
using UnityEngine.EventSystems;

public class BrothStation : FoodStation
{

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        //주우면 투명하게 만들어야함.
        //if (mouseHand.Gethand().Equal()) { }
    }
}
