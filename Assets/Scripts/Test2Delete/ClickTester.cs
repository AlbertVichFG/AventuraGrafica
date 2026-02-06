using UnityEngine;
using UnityEngine.InputSystem;

public class ClickTester : MonoBehaviour
{
    void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Clicked: " + hit.collider.name);
        }
    }
}
