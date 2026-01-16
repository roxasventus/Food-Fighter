using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    public bool isComplete;

    public enum Base
    {
        noodle,
        tteok
    }

    public enum Sauce
    {
        soup,
        jjajang
    }

    public enum Special
    {
        none,
        miwon,
        hot,
        olive
    }

    [SerializeField] private Base Recipe_Base;
    public Base GetBase { get => Recipe_Base; }

    [SerializeField] private Sauce Recipe_Sauce;

    public Sauce GetSauce { get => Recipe_Sauce; }

    [SerializeField] private Special Recipe_Special;

    public Special GetSpecial { get => Recipe_Special; }
}
