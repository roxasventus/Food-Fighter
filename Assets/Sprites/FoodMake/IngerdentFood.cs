using UnityEditor;
using UnityEngine;

public class IngerdentFood : MonoBehaviour
{
    [SerializeField]
    public Sprite FoodImage;
    public Ingredient ingredientData;

    public enum Ingredient
    {
        RiceCake,
        Cotton,
        soup,
        jjajang
    }

    public void Start()
    {
        DataSet();

    }
    public void DataSet()
    {
        this.GetComponent<SpriteRenderer>().sprite = FoodImage;
    }

}
