using UnityEngine;

[CreateAssetMenu(fileName = "FoodConsts", menuName = "Scriptable Objects/FoodConsts")]
public class FoodConsts : ScriptableObject
{
    public float throwDuration = 1f;
    public float throwMaxMult = 0.3f;
    public int foodCnt = 5;
}