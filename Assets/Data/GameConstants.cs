using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "Scriptable Objects/GameConstants")]
public class GameConstants : ScriptableObject
{
    public float boardSpeed = 4f;
    public float truckShakeX = 0.4f;
    public float truckShakeY = 0.15f;
    public float truckShakeFreq = 0.5f;

    public float focusSpeed = 1f;
    public float[] focusXClamp = {-6.65f, -1.2f};
    public float[] focusYClamp = {-4.65f, 4.65f};
}
