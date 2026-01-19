using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; // 새로운 인풋 시스템 네임스페이스 추가

public class HelpImage : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject obj;

    public void OnPointerDown(PointerEventData eventData)
    {
        obj.SetActive(true);
        StartCoroutine(Co_StayPressF());
        GameTimeManager.instance.SetGameTimeScaleWithPercent(0f);
    }

    IEnumerator Co_StayPressF()
    {
        // Keyboard.current.fKey.wasReleasedThisFrame은 
        // Input.GetKeyUp(KeyCode.F)와 동일한 역할을 합니다.

        bool isReleased = false;
        while (!isReleased)
        {
            Debug.Log("Waiting for F key release...");
            // F 키가 떼어졌는지 확인
            if (Keyboard.current != null && Keyboard.current.fKey.wasReleasedThisFrame)
            {
                isReleased = true;
            }

            yield return null;
        }

        obj.SetActive(false);
        GameTimeManager.instance.ResetGameTimeScale();
    }
}