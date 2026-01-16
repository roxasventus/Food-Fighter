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

    void Update()
    {
        
    }

    // x축으로 랜덤성 구현
    private IEnumerator XRandomize()
    {
        while (true)
        {
            float duration = ec.changeCool;
            float firstX = transform.position.x;
            float targetX = Random.Range(initPos.x - ec.changeDist, initPos.x + ec.changeDist);

            Debug.Log(targetX);
            float t = 0f;
            while (t < duration)
            {
                float vX = Mathf.Lerp(firstX, targetX, t/duration);
                transform.position = new Vector2(vX, transform.position.y);
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
                Vector2 target = new Vector2(transform.position.x, initPos.y) + delta;
                target.x = Mathf.Clamp(target.x, ec.xRange[0], ec.xRange[1]);

                transform.position = target;
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
