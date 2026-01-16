using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{

    public enum Status
    { 
        Cooking,
        fail,
        incomplete,
        complete,
    }

    public enum Base
    {
        none,
        noodle,
        tteok
    }

    public enum Sauce
    {
        none,
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

    [SerializeField] private Status Recipe_Status;
    public Status GetStatus { get => Recipe_Status; }
    public void SetStatus(Status recipe_status) { Recipe_Status = recipe_status; }

    [SerializeField] private Base Recipe_Base;
    public Base GetBase { get => Recipe_Base; }
    public void SetBase(Base recipe_base) { Recipe_Base = recipe_base; }


    [SerializeField] private Sauce Recipe_Sauce;

    public Sauce GetSauce { get => Recipe_Sauce; }
    public void SetSauce(Sauce recipe_sauce) { Recipe_Sauce = recipe_sauce; }


    [SerializeField] private Special Recipe_Special;

    public Special GetSpecial { get => Recipe_Special; }
    public void SetSpecial(Special recipe_special) { Recipe_Special = recipe_special; }
}
