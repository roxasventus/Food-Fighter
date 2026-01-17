using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using static IngerdentFood;

public class BrothStation : FoodStation
{

    SpriteRenderer spriteRenderer;
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (mouseHand.Gethand() != null)
            mouseHand.Gethand().GetComponent<IEntity>().SelfRelease();

        GameObject go = ObjPoolManager.instance.InstantiateFromPool(IngerdentPrefab.name.ToString());
        go.transform.position = transform.position;
        mouseHand.Sethand(go);
    }
    public void Start()
    {
        setAtion();
    }
    public void Update()
    {
        HideAction();

    }
    public void setAtion()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void HideAction()
    {
        if (go != null)
        {
            if (go.activeSelf == false)
            {
                spriteRenderer.color = new UnityEngine.Color(1f, 1f, 1f, 1f);
            }
            else spriteRenderer.color = new UnityEngine.Color(1f, 1f, 1f, 0f);
        } else { spriteRenderer.color = new UnityEngine.Color(1f, 1f, 1f, 1f); }
            
       

    }
}
