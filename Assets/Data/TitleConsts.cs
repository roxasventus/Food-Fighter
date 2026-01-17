using UnityEngine;

[CreateAssetMenu(fileName = "TitleConsts", menuName = "Scriptable Objects/TitleConsts")]
public class TitleConsts : ScriptableObject
{
    public float rattleCool = 5f;
    public float rattleAngle = 4f;
    public float rattleDuration = 0.2f;
    public float rattleCnt = 3;

    public float openDuration = 0.6f;

    public float slidingSpeed = 2f;
}
