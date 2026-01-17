using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TruckMove : MonoBehaviour
{
    [SerializeField] private RectTransform truckPanel;
    [SerializeField] private float slideDuration = 0.3f;
    [SerializeField] private int truckMoveMode;

    private Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        StartCoroutine(TruckMoveCoroutine(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startTruckMove(int truckMoveMode) {
        StartCoroutine(TruckMoveCoroutine(truckMoveMode));
    }

    IEnumerator TruckMoveCoroutine(int index)
    {
        Vector2 startPos = Vector2.zero;
        Vector2 endPos = Vector2.zero;

        Quaternion startRotation = Quaternion.identity;
        Quaternion endRotation = Quaternion.identity;

        if (index == 0) {
            startPos = new Vector2(0, 0);
            endPos = new Vector2(-Screen.width / 4, 0);

        }

        if (index == 1)
        {
            startPos = new Vector2(-Screen.width / 1.8f, 0);
            endPos = new Vector2(-Screen.width / 1.5f, Screen.height / 2);

            startRotation = transform.rotation;
            endRotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, -90));

        }

        if (index == 2)
        {
            startPos = new Vector2(-Screen.width / 1.8f, 0);
            endPos = new Vector2(-Screen.width, 0);

            startRotation = transform.rotation;
            endRotation = transform.rotation;

        }

        if (index == 3)
        {
            startPos = new Vector2(-Screen.width / 1.8f, 0);
            endPos = new Vector2(-Screen.width / 1.5f, -Screen.height / 2);

            startRotation = transform.rotation;
            endRotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 90));
        }

        float time = 0f;

        truckPanel.anchoredPosition = startPos;

        while (time < slideDuration)
        {
            time += Time.deltaTime;
            float t = time / slideDuration;

            truckPanel.anchoredPosition =
                Vector2.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / slideDuration);

            yield return null;
        }

        truckPanel.anchoredPosition = endPos;


    }


}
