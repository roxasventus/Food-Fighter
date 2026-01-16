using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConsts", menuName = "Scriptable Objects/EnemyConsts")]
public class EnemyConsts : ScriptableObject
{
    [Header("General")]
    public float[] xRange = {-6.9f, -0.8f};
    [Header("직선으로 다가오는 좀비")]
    public float moveSpeed = 5f;

    [Header("곡선으로 다가오는 좀비")]
    [Tooltip("진폭")]
    public float moveAmplitude = 3f;
    [Tooltip("주기")]
    public float moveFrequency = 1.0f;

    [Tooltip("x축 방향으로 랜덤성")]
    public float changeCool = 0.2f;
    public float[] changeRange = {5, 50};
}
