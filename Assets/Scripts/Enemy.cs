using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyConsts ec;
    
    private bool isStraight = false;
    // 곡선 좀비
    private Vector2 initPos;
    private float offset;
    private float elapsedTime = 0f;

    public void Init(Vector2 pos, bool isSt)
    {
        initPos = pos;
        offset = GetCubicWobblySlope(0, ec.moveAmplitude, ec.moveFrequency);
        transform.position = pos;
        isStraight = isSt;

        if (!isStraight)
        {
            StartCoroutine(XRandomize());
            StartCoroutine(YShake());
        }
    }

    void Start()
    {
        Init(new Vector2(-4f, 5.3f), false);
    }

    // x축으로 랜덤성 구현
    private IEnumerator XRandomize()
    {
        while (true)
        {
            float duration = ec.changeCool;

            // 랜덤 방향 (0 or 1 -> -1 or 1)
            int r = Random.Range(0, 2);
            int dir = (r == 0) ? 1 : -1;

            // 랜덤 거리
            float dist = Random.Range(ec.changeRange[0], ec.changeRange[1]);

            // 이번 턴에 이동할 총 거리 (방향 포함)
            float totalMove = dir * dist;
            float moveSpeed = totalMove / duration;

            float t = 0f;
            while (t < duration)
            {
                float step = moveSpeed * Time.deltaTime;
                initPos.x += step;
                t += Time.deltaTime;
                yield return null;
            }
        }
    }

    private IEnumerator YShake()
    {
        while (true)
        {
            if (isStraight)
            {
                Vector2 delta = Vector2.down * ec.moveSpeed * Time.deltaTime;
                transform.position += (Vector3)delta;
            }
            else
            {
                elapsedTime += Time.deltaTime;
                Vector2 delta = Vector2.down * (GetCubicWobblySlope(elapsedTime, ec.moveAmplitude, ec.moveFrequency) - offset);

                // Debug.Log(delta);
                Vector2 target = initPos + delta;
                target.x = Mathf.Clamp(target.x, ec.xRange[0], ec.xRange[1]);
            }

            yield return null;
        }
    }

    // 삼차함수 파동 구현
    private float Smoothstep(float t) 
    {
        return t * t * (3f - 2f * t);
    }

    float GetWobble(float x, float frequency) 
    {
        float t = (x * frequency) % 1f; // 0~1 반복 (톱니파)
        float triangle = Mathf.Abs(t * 2f - 1f); // 1->0->1 삼각형 모양
        return Smoothstep(1f - triangle); // 0->1->0 삼차곡선
    }

    public float GetCubicWobblySlope(float x, float amplitude, float frequency) 
    {
        float slope = ec.moveSpeed;
        // 1. 우하향 기본 추세
        float linearTrend = slope * x;

        // 2. 삼차함수 기반 일렁임
        float wobble = GetWobble(x, frequency) * amplitude;

        // 3. 합성 (사인파처럼 중심을 맞추려면 amplitude의 절반을 빼주는 등 조정 가능)
        return linearTrend + wobble;
    }
}
