using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
    public float yOffset = 32.4f;
    private Vector2 originPos;
    [SerializeField] GameConstants gc;
    [SerializeField] Transform group1;
    [SerializeField] Transform group2;
    [SerializeField] Transform truck;

    void Start()
    {
        originPos = truck.position;

        StartCoroutine(TruckShake());
    }
    void Update()
    {
        BoardScroll();
    }

    private void BoardScroll()
    {
        Vector3 delta = Vector3.up * gc.boardSpeed * Time.deltaTime;
        group1.position += delta;
        group2.position += delta;

        if (group1.position.y > yOffset * 0.9f)
        {
            group1.position += Vector3.down * yOffset * 2;
        }

        if (group2.position.y > yOffset * 0.9f)
        {
            group2.position += Vector3.down * yOffset * 2;
        }
    }

    IEnumerator TruckShake()
    {
        float elapsed = 0f;
        float duration = gc.truckShakeFreq;
        while (true)
        {
            elapsed = 0f;
            Vector2 firstPos = truck.position;
            Vector2 targetPos = new Vector2(
                Random.Range(originPos.x - gc.truckShakeX, originPos.x + gc.truckShakeX),
                Random.Range(originPos.y - gc.truckShakeY, originPos.y + gc.truckShakeY)
            );

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                truck.position = Vector2.Lerp(firstPos, targetPos, elapsed/duration);
                
                yield return null;
            }
        }
    }
}
