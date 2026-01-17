using UnityEngine;

public class Controller : MonoBehaviour
{
    private InputActions inputActions;
    private Vector2 moveInput;

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
    }

    void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }
}
