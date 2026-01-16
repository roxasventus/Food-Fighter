using System;
using UnityEngine;
using static IngerdentFood;

public class Pot : MonoBehaviour
{
    Recipe recipe = new Recipe();
    Recipe.Base Base;
    Recipe.Sauce Sauce;

    private float Cooktime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReSet()
    {
        //null ÇÊ¿ä.
        Base = Recipe.Base.noodle;
        Sauce= Recipe.Sauce.jjajang;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enum collEnum = collision.GetComponent<IngerdentFood>().ingredientData;
        if (collEnum != null) 
        {
            
        }
      

    }

}
