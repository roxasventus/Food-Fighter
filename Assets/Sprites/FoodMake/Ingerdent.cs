using UnityEditor;
using UnityEngine;

public class Ingerdent : MonoBehaviour
{
    [SerializeField]
    public Sprite FoodImage;
    public Ingredient ingredientData;


    public void Start()
    {
        DataSet();

    }
    public void Update()
    {
        transform.position = Input.mousePosition;
    }
    public void DataSet()
    {
        this.GetComponent<SpriteRenderer>().sprite = FoodImage;
    }
    public enum Ingredient
    {
        RiceCake,
        Cotton,
        soup,
        jjajang
    }

}
