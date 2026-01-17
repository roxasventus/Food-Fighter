using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] FoodConsts fc;
    [SerializeField] GameConstants gc;

    [SerializeField] GameObject eff;

    private bool isInit = false;

    public int countdown;
    public FavoriteFood type;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isInit)
        {
            transform.position += Vector3.up * gc.boardSpeed * Time.deltaTime;
        }
    }

    public bool CheckY() // true: 벗어남
    {
        if (transform.position.y >= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Init(Sprite s, Vector2 start, Vector2 end)
    {
        eff.SetActive(false);
        isInit = false;
        countdown = fc.foodCnt;

        sr.sprite = s;
        transform.position = start;
        StartCoroutine(Throw(start, end));
    }

    private IEnumerator Throw(Vector2 start, Vector2 end)
    {
        float elapsed = 0f;
        float duration = fc.throwDuration;

        while (elapsed < duration)
        {
            // 위치 변경
            transform.position = Vector2.Lerp(start, end, elapsed/duration);

            // 스케일 변경
            float p = 1 - (Mathf.Abs(elapsed - duration/2) * 2); // 0 ~ 1 ~ 0
            transform.localScale = Vector3.one + Vector3.one * fc.throwMaxMult * p;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
        isInit = true;
        eff.SetActive(true);
    }

    public void Release()
    {
        ObjPoolManager.instance.Release(gameObject, "Food");
    }
}
