using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private bool m_shake_on_Idle = false;
    public bool ShakeOnIdle { get { return m_shake_on_Idle; } set { m_shake_on_Idle = value; } }
    [SerializeField] private float power = 1f;
    public float Power { get { return power; } set { power = value; } }
    [SerializeField] private Vector2 range = new Vector2(0.1f, 0.5f);
    public Vector2 Range { get { return range; } set { range = value; } }
    
    Vector3 defaultPos;

    public bool isActive = false;

    //public static CameraShaker Instance;
    void Awake()
    {
        //Instance = this;
        defaultPos = new Vector3(0f, 0f, -10f);
        m_timer = UnityEngine.Random.Range(range.x, range.y);
    }

    float m_timer = 0f;
    void LateUpdate()
    {
        if(!m_shake_on_Idle) return;

        if (!isActive) return;

        m_timer -= Time.deltaTime;
        if(m_timer <= 0f)
        {
            Shake_Y(Power);
            m_timer = UnityEngine.Random.Range(range.x, range.y);
        }
    }



    // 잔돌 넘을 때
    public void Shake_Y(float power)
    {
        StopAllCoroutines();
        StartCoroutine(Co_Shake_Y(power));
    }

    IEnumerator Co_Shake_Y(float power)
    {
        float timer = 0.4f * power;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            float offsetY = Time.deltaTime * power * 20f;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + offsetY,transform.localPosition.z);
            yield return null;
        }
        timer = 0.1f * power;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            float offsetY = -Time.deltaTime * power * 90f;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + offsetY,transform.localPosition.z);
            yield return null;
        }
        transform.localPosition = defaultPos;
    }
}
