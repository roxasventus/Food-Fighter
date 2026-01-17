using System.Collections;
using UnityEngine;

public class TitleBoard : MonoBehaviour
{
    [SerializeField] TitleConsts tc;

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject hp;

    void Start()
    {
        StartCoroutine(Slide());
    }

    IEnumerator Slide()
    {
        // 카메라 원점 이동
        while (Camera.main.transform.position.x < 0)
        {
            Camera.main.transform.position += Vector3.right * tc.slidingSpeed * Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);

        // 트럭 이동
        while (transform.position.x > -36.5f)
        {
            transform.position += Vector3.left * tc.slidingSpeed * Time.deltaTime;
            yield return null;
        }

        Camera.main.GetComponent<CameraShaker>().isActive = true;

        canvas.SetActive(true);
        hp.SetActive(true);
        
        // 게임 시작 >> 엔트리포인트
        GameManager.instance.GameStart();
    }
}
