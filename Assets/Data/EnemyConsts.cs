using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConsts", menuName = "Scriptable Objects/EnemyConsts")]
public class EnemyConsts : ScriptableObject
{
    [Header("General")]
    [Tooltip("좀비가 위치할 수 있는 좌표 범위")]
    public float[] xRange = {-6.9f, -0.8f};
    public float crashDuration = 0.3f;

    [Header("Spawner")]
    [Tooltip("좀비가 스폰하는 좌표 범위")]
    public float[] spawnRange = {-6.3f, -1.3f}; // changeDist 참고하기!
    public float spawnY = 6.5f;
    public float jumpY = -1.7f; // 트럭 돌진 판정 y
    
    [Header("직선으로 다가오는 좀비")]
    public float moveSpeed = 5f;

    [Header("곡선으로 다가오는 좀비")]
    [Tooltip("진폭")]
    public float moveAmplitude = 3f; // 돌아가는 범위
    [Tooltip("주기")]
    public float moveFrequency = 1.0f; // x초마다 흔들림

    [Tooltip("x축 방향으로 랜덤성")]
    public float changeCool = 0.2f;
    public float changeDist = 0.5f;

    [Tooltip("템플릿 생성할 때 같이 생성될 때 기다리는 시간 범위")]
    public float[] templateGapRange = {0.1f, 0.3f};
}
