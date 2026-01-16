using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngerdentFood : MonoBehaviour
{
    [SerializeField]
    public Sprite FoodImage;
    public Ingredient ingredientData;

    public enum Ingredient
    {
        noodle,
        tteok,

        soup,
        jjajang,

        Broth,

        miwon,
        hot,
        olive
    }

    public void Start()
    {
        DataSet();

    }
    public void DataSet()
    {
        //this.GetComponent<SpriteRenderer>().sprite = FoodImage;
    }
    public void SlefRelease()
    {
        ObjPoolManager.instance.Release(this.gameObject, gameObject.name.ToString().Replace("(Clone)",""));

    }
}
