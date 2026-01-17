using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("기본 정보")]
    public int id;
    public string enemy_name;

    [Header("설정 값")]
    public float moveSpeedRate;
    public bool moveStraight;
    public float xMoveRate;
    public FavoriteFood favorite;

    public AnimatorOverrideController anim;
}

public enum FavoriteFood
{
    Eth,
    RM,
    JRM,
    TB,
    JTB
}