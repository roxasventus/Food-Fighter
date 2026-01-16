using Unity.VisualScripting;
using UnityEngine;
using static IngerdentFood;

public class FoodStation : MonoBehaviour
{
    public GameObject IngerdentPrefab;


    public void OnMouseDown()
    {
        GameObject Food = Instantiate(IngerdentPrefab);
        Food.transform.position = transform.position;
    }
}
