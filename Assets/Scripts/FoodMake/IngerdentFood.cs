using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngerdentFood : MonoBehaviour, IEntity
{
    [SerializeField]
    public Ingredient ingredientData;

    public enum Ingredient
    {
        noodle,
        tteok,

        soup,
        jjajang,

        Broth,

        greenOnionsEggs,

        miwon,
        hot,
        olive
    }

    public void EntityReset()
    {

    }
    public void SelfRelease()
    {
        ObjPoolManager.instance.Release(gameObject, gameObject.name.ToString().Replace("(Clone)",""));

    }


}
