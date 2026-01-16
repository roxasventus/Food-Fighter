using Unity.VisualScripting;
using UnityEngine;

public class FoodStation : MonoBehaviour
{
    public Food PutinFood;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        GameObject Food = Instantiate(PutinFood).GameObject();
        Food.transform.position = transform.position;
    }
}
