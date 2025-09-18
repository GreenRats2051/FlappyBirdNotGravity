using UnityEngine;

public class UnityInputService : IInputService
{
    public bool IsHoldingInput()
    {
        return Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);
    }
}