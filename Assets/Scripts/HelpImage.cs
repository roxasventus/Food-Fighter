using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HelpImage : MonoBehaviour
{
    [SerializeField] Image img;

    void OnMouseOver()
    {
        // | Ani
    }
    void OnMouseDown()
    {
        // 도움말 등장.
        img.enabled = true;
        GameTimeManager.instance.SetGameTimeScaleWithPercent(0f);
    }

    IEnumerator Co_StayPressF()
    {
        // F 키를 뗄 때까지 루프
        while (!Input.GetKeyUp(KeyCode.F))
        {
            // 타임스케일의 영향을 받지 않는 '실제 프레임' 단위로 대기
            yield return new WaitForEndOfFrame();
        }

        // 도움말 사라짐
        img.enabled = false;

        // 게임 속도 정상 복구
        GameTimeManager.instance.ResetGameTimeScale();
    }
}