using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHand : MonoBehaviour
{
    // 마우스 정보.
    public  GameObject  handIngerdentFood = null;

    void LateUpdate()
    {
        if (handIngerdentFood != null)
        {
            Vector3 mouseScreen = Mouse.current.position.ReadValue();
            Vector3 mp = new Vector3(mouseScreen.x, mouseScreen.y, -Camera.main.transform.position.z);
            Vector3 world = Camera.main.ScreenToWorldPoint(mp);

            handIngerdentFood.transform.position = world;
        }

    }
    public void Sethand(GameObject ingerdent) 
    {
        handIngerdentFood = ingerdent;
    }
}
