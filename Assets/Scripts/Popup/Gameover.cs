using UnityEngine;
using TMPro;
using System.Collections;
public class Gameover : MonoBehaviour
{
    [SerializeField] PopupConsts pc;

    [SerializeField] RectTransform rt;

    [SerializeField] TMP_Text stage;
    [SerializeField] TMP_Text burn;
    [SerializeField] TMP_Text made;
    [SerializeField] TMP_Text dist;
    [SerializeField] TMP_Text zomcnt;

    [SerializeField] GameObject canvas;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void StartShow()
    {
        Time.timeScale = 0f;

        // text 값 설정하기
        stage.text = $"{15 - GameManager.instance.roundCount}";
        burn.text = "미구현...";
        made.text = $"";

        canvas.SetActive(true);

        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        // 창 보이기
        Vector3 start = new Vector3(0f, -1000f, 0f);
        Vector3 end = Vector3.zero;

        rt.anchoredPosition = start;
        yield return null;

        float elapsed = 0f;
        float duration = pc.gameoverShowDuration;

        while (elapsed < duration)
        {
            rt.anchoredPosition = Vector3.Lerp(start, end, elapsed/duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        rt.anchoredPosition = end;

        // 글자 페이드인
        yield return StartCoroutine(FadeIn(stage));
        yield return StartCoroutine(FadeIn(burn));
        yield return StartCoroutine(FadeIn(made));
        // 같이 보이기
        StartCoroutine(FadeIn(dist));
        yield return StartCoroutine(FadeIn(zomcnt));
    }

    IEnumerator FadeIn(TMP_Text t)
    {
        Color origin = t.color;
        Color target = new Color(origin.r, origin.g, origin.b, 1f);

        float elapsed = 0f;
        float duration = pc.textShowDuration;

        while (elapsed < duration)
        {
            t.color = Color.Lerp(origin, target, elapsed/duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
