using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int itemIndex;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemIndex == 0 &&  GameManager.instance.getMiwon > 0)
        {
            Debug.Log("1");
            GameManager.instance.setItem(itemIndex);
        }
        if (itemIndex == 1 && GameManager.instance.getHot > 0)
        {
            Debug.Log("2");
            GameManager.instance.setItem(itemIndex);
        }
        if (itemIndex == 2 && GameManager.instance.getOlive > 0)
        {
            Debug.Log("3");
            GameManager.instance.setItem(itemIndex);
        }

    }
}
