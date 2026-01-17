using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private RectTransform transitionPanel;
    [SerializeField] private float slideDuration = 4.0f;

    public static SceneLoader Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void StartIntro()
    {

        StartCoroutine(StartIntoCoroutine());
    }

    public void StartGame()
    {

        StartCoroutine(EndIntoCoroutine());
    }

    IEnumerator StartIntoCoroutine()
    {

        Vector2 startPos = new Vector2(Screen.width, 0);
        Vector2 endPos = Vector2.zero;

        float time = 0f;

        transitionPanel.anchoredPosition = startPos;

        while (time < slideDuration)
        {
            time += Time.deltaTime;
            float t = time / slideDuration;

            transitionPanel.anchoredPosition =
                Vector2.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));

            yield return null;
        }

        transitionPanel.anchoredPosition = endPos;

        // 씬 로드
        Load("GameScene");
    }

    IEnumerator EndIntoCoroutine()
    {

        Vector2 startPos = Vector2.zero;
        Vector2 endPos = new Vector2(-Screen.width, 0);

        float time = 0f;

        transitionPanel.anchoredPosition = startPos;

        while (time < slideDuration)
        {
            time += Time.deltaTime;
            float t = time / slideDuration;

            transitionPanel.anchoredPosition =
                Vector2.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));

            yield return null;
        }

        transitionPanel.anchoredPosition = endPos;
    }
}

