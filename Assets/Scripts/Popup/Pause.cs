using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Pause : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private RectTransform rt;
    [SerializeField] PopupConsts pc;
    [SerializeField] TMP_Text display;
    [SerializeField] RectTransform popup;
    [SerializeField] Canvas pauseCanvas;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Expand());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rt.localScale = Vector3.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 1f;
        popup.anchoredPosition = new Vector3(332f, -1100f, 0f);
        pauseCanvas.gameObject.SetActive(false);
    }

    public void StartShow()
    {
        Time.timeScale = 0f; // 시간 정지!
        pauseCanvas.gameObject.SetActive(true);
        StartCoroutine(Show());
    }

    private IEnumerator Expand()
    {
        Vector3 first = Vector3.one;
        Vector3 target = first * pc.returnBtnScale;

        float elapsed = 0f;
        float duration = pc.returnBtnDuration;

        while (elapsed < duration)
        {
            rt.localScale = Vector3.Lerp(first, target, elapsed / duration);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private IEnumerator Show()
    {
        //display.text = GameManager.instance.GetTotalTimeString();

        Vector3 start = new Vector3(332f, -1100f, 0f);
        Vector3 end = new Vector3(332f, -55f, 0f);

        float elapsed = 0f;
        float duration = pc.pauseShowDuration;

        while (elapsed < duration)
        {
            popup.anchoredPosition = Vector3.Lerp(start, end, elapsed/duration);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
