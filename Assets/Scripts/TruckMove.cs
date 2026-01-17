using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TruckMove : MonoBehaviour
{
    [SerializeField] private RectTransform truckPanel;
    [SerializeField] private float slideDuration = 0.3f;

    private Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        StartCoroutine(BusMoveCoroutine(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BusMoveCoroutine(int index)
    {
        Vector2 startPos = Vector2.zero;
        Vector2 endPos = Vector2.zero;

        if (index == 0)
        {
            startPos = new Vector2(0, 0);
            endPos = new Vector2(-Screen.width / 4, 0);
        }

        else if (index == 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            truckPanel.Rotate(0, 0, -90);

            Debug.Log(truckPanel. localPosition.x);
            Debug.Log(truckPanel.position.y);

            startPos = new Vector2(-Screen.width / 1.8f , 0);
            endPos = new Vector2(-Screen.width / 1.5f, Screen.height/2);
        }

        float time = 0f;

        truckPanel.anchoredPosition = startPos;

        while (time < slideDuration)
        {
            time += Time.deltaTime;
            float t = time / slideDuration;

            truckPanel.anchoredPosition =
                Vector2.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));

            yield return null;
        }

        truckPanel.anchoredPosition = endPos;

        if (index == 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f); 
                ;
            StartCoroutine(BusMoveCoroutine(1));
        }
        else if (index == 1)
        {
            gameObject.SetActive(true);
        }
    }


}
