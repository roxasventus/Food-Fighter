using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Lid : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] TitleConsts tc;
    [SerializeField] Transform truck;
    bool canClick = true;

    private Coroutine rattleCoroutine;
    private AsyncOperation op;
    void Start()
    {
        rattleCoroutine = StartCoroutine(Rattle());
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canClick) return;
        canClick = false;
        SoundManager.instance.PlaySound("PotPlace",1f);
        op = SceneManager.LoadSceneAsync("GameScene");
        op.allowSceneActivation = false;
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        StopCoroutine(rattleCoroutine);
        Vector2 startPos = transform.position;
        Vector3 startAng = Vector3.zero;

        Vector2 endPos = new Vector2(4f, 3.7f);
        Vector3 endAng = new Vector3(0f, 0f, -60f);

        float elapsed = 0f;
        float duration = tc.openDuration;

        while (elapsed < duration)
        {
            transform.position = Vector2.Lerp(startPos, endPos, elapsed/duration);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(startAng, endAng, elapsed/duration));
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        canClick = true;

        // 트럭 나오기
        while (truck.position.x > 11.75f)
        {
            truck.position += Vector3.left * tc.slidingSpeed * Time.deltaTime;
            yield return null;
        }

        // 씬 전환하기
        yield return StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        while (op.progress < 0.9f)
        {
            yield return null;
        }

        op.allowSceneActivation = true;
    }

    private IEnumerator Rattle()
    {
        while (true)
        {
            yield return StartCoroutine(Rotate(tc.rattleAngle, tc.rattleDuration));

            for (int i=0; i<tc.rattleCnt; i++)
            {
                yield return StartCoroutine(Rotate(-tc.rattleAngle * 2, tc.rattleDuration * 2));
                yield return StartCoroutine(Rotate(tc.rattleAngle * 2, tc.rattleDuration * 2));
            }

            yield return StartCoroutine(Rotate(-tc.rattleAngle, tc.rattleDuration));

            yield return new WaitForSeconds(tc.rattleCool);
        }
    }

    private IEnumerator Rotate(float angle, float duration)
    {
        float elapsed = 0f;
        
        float first = transform.rotation.z;
        float target = first + angle;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(first, target, elapsed/duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

         transform.rotation = Quaternion.Euler(0f, 0f, target);
    }
}
