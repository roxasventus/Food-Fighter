using Unity.VisualScripting;
using UnityEngine;

public class MouseHand : MonoBehaviour
{
    // 마우스 정보.
    public static IngerdentFood handIngerdentFood = null;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        handIngerdentFood.transform.position = Input.mousePosition;
    }
    public void Sethand(IngerdentFood ingerdent) 
    {
        handIngerdentFood = ingerdent;
    }
}
