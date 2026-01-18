using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Item : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int itemIndex;

    [SerializeField] private TMP_Text counterText;


    public void setcounterText(int num) {
        counterText.text = num.ToString();
    }

    public void buttonScaleInit() {

        gameObject.transform.localScale = Vector3.one;

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (itemIndex == 0 &&  GameManager.instance.getMiwon > 0)
        {
            Debug.Log("1");
            GameManager.instance.allButtonScaleInit();
            gameObject.transform.localScale = Vector3.one * 1.3f;
            GameManager.instance.setItem(itemIndex);
        }
        if (itemIndex == 1 && GameManager.instance.getHot > 0)
        {
            Debug.Log("2");
            GameManager.instance.allButtonScaleInit();
            gameObject.transform.localScale = Vector3.one * 1.3f;
            GameManager.instance.setItem(itemIndex);
        }
        if (itemIndex == 2 && GameManager.instance.getOlive > 0)
        {
            Debug.Log("3");
            GameManager.instance.allButtonScaleInit();
            gameObject.transform.localScale = Vector3.one * 1.3f;
            GameManager.instance.setItem(itemIndex);
        }

    }
}
