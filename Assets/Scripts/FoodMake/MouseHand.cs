using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHand : MonoBehaviour
{
    // ���콺 ����.
    private static GameObject handObject = null;

    void LateUpdate()
    {

        if (handObject != null)
        {
            Vector3 mouseScreen = Mouse.current.position.ReadValue();
            Vector3 mp = new Vector3(mouseScreen.x, mouseScreen.y, -Camera.main.transform.position.z);
            Vector3 world = Camera.main.ScreenToWorldPoint(mp);

            handObject.transform.position = world;
        }

    }

    public void Sethand(GameObject ingerdent) 
    {
        handObject = ingerdent;
    }
    public GameObject Gethand()
    {
        return handObject;
    }
}
