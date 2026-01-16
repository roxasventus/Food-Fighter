using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHand : MonoBehaviour
{
    // 마우스 정보.
    public IngerdentFood handIngerdentFood = null;


    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        if (handIngerdentFood != null)
        {
            Vector3 mouseScreen = Mouse.current.position.ReadValue();
            Vector3 mp = new Vector3(mouseScreen.x, mouseScreen.y, -Camera.main.transform.position.z);
            Vector3 world = Camera.main.ScreenToWorldPoint(mp);

            handIngerdentFood.transform.position = world;
        }

    }
    public void Sethand(IngerdentFood ingerdent) 
    {
        if (handIngerdentFood != null)
            Destroy(handIngerdentFood.gameObject);

        handIngerdentFood = ingerdent;
    }
}
