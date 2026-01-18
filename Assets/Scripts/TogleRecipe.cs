using UnityEngine;
using UnityEngine.InputSystem;

public class TogleRecipe : MonoBehaviour
{
    bool isOpen = false;
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasReleasedThisFrame)
        {
            transform.GetChild(0).gameObject.SetActive(!isOpen);
        }
    }
}
