using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lid : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] TitleConsts tc;
    [SerializeField] Transform another;

    private Coroutine rattleCoroutine;
    void Start()
    {
        rattleCoroutine = StartCoroutine(Rattle());
    }
    public void OnPointerDown(PointerEventData eventData)
    {
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
