using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyConsts ec;
    
    private bool isStraight = false;
    // 곡선 좀비
    private Vector2 initPos;
    private float offset;
    private float elapsedTime = 0f;

    private Coroutine xCor, yCor;

    // 좀비 데이터 설정값
    private float speedRate;
    private float xRate;
    public FavoriteFood favorite;

    private bool isInit = false;

    private Animator anim;
    private SpriteRenderer sr;

    //| SOUND
    private string voiceClipName;

    void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector2 pos, EnemyData data)
    {
        anim.runtimeAnimatorController = data.anim;

        speedRate = data.moveSpeedRate;
        xRate = data.xMoveRate;
        favorite = data.favorite;
        isStraight = data.moveStraight;
        voiceClipName = data.Voice;

        initPos = pos;
        elapsedTime = Random.Range(0, ec.moveFrequency);
        offset = GetCubicWobblySlope(0, ec.moveAmplitude, ec.moveFrequency);
        transform.position = pos;
        transform.rotation = Quaternion.identity;

        xCor = StartCoroutine(XRandomize());
        if (!isStraight)
        {
            yCor = StartCoroutine(YShake());
        }

        isInit = true;

        StartCoroutine(Co_PlayVoiceOnWhile()); //| SOUND

        // 임시용
        /*Color c;
        switch (favorite)
        {
            case FavoriteFood.RM:
                c = Color.orange;
                break;
            case FavoriteFood.JRM:
                c = Color.brown;
                break;
            case FavoriteFood.TB:
                c = Color.red;
                break;
            case FavoriteFood.JTB:
                c = Color.black;
                break;
            default:
                c = Color.white;
                break;
        }

        GetComponent<SpriteRenderer>().color = c;*/
    }

    void Update()
    {
        if (isStraight)
        {
            transform.position += Vector3.down * ec.moveSpeed * Time.deltaTime * speedRate;
        }
    }

    public bool isCrash() // 트럭에 돌진하는 좌표인가?
    {
        return transform.position.y <= ec.jumpY && isInit;
    }

    int BreathYOffset = 3;
    public bool isBreathSound() // 숨소리가 들리는 좌표인가?
    {
        return transform.position.y <= (ec.jumpY  + BreathYOffset) && isInit;
    }

    public IEnumerator FoundFood(Transform food, Transform manager)
    {
        //| VFX
        VFX_Manager.i.PlayVFX("ZombieKill", transform.position, Quaternion.identity);
        //| SOUND
        SoundManager.instance.PlaySound("ZombieHighSound" + Random.Range(1,4).ToString(), 1f);
        // 움직임 멈추기
        StopCoroutine(xCor);
        if (!isStraight)
            StopCoroutine(yCor);

        anim.SetTrigger("SideJump");
        
        // 자식으로 만들기
        transform.SetParent(food);
        
        float elapsed = 0f;
        float duration = ec.crashDuration;
        Vector2 first = transform.localPosition;
        
        if (first.x > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    
        while (elapsed < duration)
        {
            transform.localPosition = Vector2.Lerp(first, Vector2.zero, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        while (transform.position.y < 9)
        {
            yield return null;
        }
        
        transform.SetParent(manager);
        Release();
    }
    public IEnumerator Crash(Vector2 truck)
    {
        StopCoroutine(xCor);
        if (!isStraight)
            StopCoroutine(yCor);

        anim.SetTrigger("Jump");

        // -3.8 -4.4
        float elapsed = 0f;
        float duration = ec.crashDuration;
        Vector2 firstPos = transform.position;
        Vector2 targetPos = truck;

        while (elapsed <= duration)
        {
            transform.position = Vector2.Lerp(firstPos, targetPos, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 데미지 처리
        GameManager.instance.loseLife();
        Debug.Log("Crash!!");
        Release();
    }

    public void Release() // 돌진 후 사라지기
    {
        isInit = false;
        ObjPoolManager.instance.Release(gameObject, "Enemy");
    }

    // x축으로 랜덤성 구현
    private IEnumerator XRandomize()
    {
        while (true)
        {
            float duration = ec.changeCool;
            float firstX = transform.position.x;
            float targetX = Random.Range(initPos.x - (ec.changeDist * xRate), initPos.x + (ec.changeDist * xRate));

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




     // ========================================
     //| SOUND
     // ========================================
     IEnumerator Co_PlayVoiceOnWhile()
     {
        while (isInit)
        {
            yield return new WaitForSeconds(Random.Range(0f, 2f));
            SoundManager.instance.PlaySound(voiceClipName + Random.Range(1, 4).ToString());
        }
     }

}
