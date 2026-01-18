using UnityEngine;
using UnityEngine.InputSystem;

public class TogleRecipe : MonoBehaviour
{
    bool isOpen = false;
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            // KeyDown처럼 누른 순간에만 실행됩니다.
            isOpen = !isOpen;
            transform.GetChild(0).gameObject.SetActive(isOpen);
        }
    }
}
