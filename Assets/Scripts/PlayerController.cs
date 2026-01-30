using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera mainCamera;
    [Header("References")]
    [SerializeField] VirtualCursorController cursor;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Click del ratolí
        bool mouseClick = Mouse.current.leftButton.wasPressedThisFrame;
        // Botó A del gamepad
        bool gamepadClick = Gamepad.current != null &&
                            Gamepad.current.buttonSouth.wasPressedThisFrame;

        if (!mouseClick && !gamepadClick)
            return;

        //Crear raig i comprovar si colisona amb colider i tag
        Ray ray = mainCamera.ScreenPointToRay(GetPointerPosition());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.CompareTag("WalkZone"))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    Vector2 GetPointerPosition()
    {
        if (Gamepad.current != null)
        {
            //PunterVirutal i agafar posicio al clickar

        }
      

        return Mouse.current.position.ReadValue();
    }
}
