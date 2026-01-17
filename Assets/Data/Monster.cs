using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    public enum MoveType
    {
        sin,
        lerp
    }

    public enum Favorite_food
    {
        everything,
        lamyeon,
        jjajanglamyeon,
        tteokbokki,
        jjajangtteokbokki
    }


    [SerializeField] private float X_Value;
    public float Get_X_Value { get => X_Value; }

}
