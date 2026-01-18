using UnityEngine;

[CreateAssetMenu(fileName = "PopupConsts", menuName = "Scriptable Objects/PopupConsts")]
public class PopupConsts : ScriptableObject
{
    [Header("일시정지 팝업")]
    public float returnBtnScale = 1.2f;
    public float returnBtnDuration = 0.5f;

    public float pauseShowDuration = 0.5f;

    [Header("게임오버 팝업")]
    public float gameoverShowDuration = 0.7f;
    public float textShowDuration = 0.3f;
}
