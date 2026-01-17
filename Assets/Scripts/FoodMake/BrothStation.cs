using UnityEngine;
using UnityEngine.EventSystems;

public class BrothStation : FoodStation
{
    [SerializeField]
    SpriteRenderer sr;
    public bool isHide = false;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnValidate()
    {
        if (go != null)
            isHide = true;
        else
            isHide = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

    }
    
}
