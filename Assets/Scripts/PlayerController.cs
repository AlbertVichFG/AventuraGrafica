using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private VirtualCursorController cursor;

    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float doubleClickTime;
    [SerializeField] private float navMeshSampleRadius;
    [SerializeField] private LayerMask walkZoneLayer;

    private NavMeshAgent agent;
    private Camera mainCamera;

    private float lastClickTime;
    private bool runRequested;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        agent.speed = walkSpeed;
    }

    void Update()
    {
        HandleInput();
    }

    void LateUpdate()
    {
        //rescatar si es queda fora navmesh
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 0.3f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
    }

    void HandleInput()
    {
        bool click =
            Mouse.current.leftButton.wasPressedThisFrame ||
            (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame);

        //si no click res
        if (!click)
            return;

        // Detect dbl click
        if (Time.time - lastClickTime <= doubleClickTime)
        {
            runRequested = true;
        }
        else
        {
            runRequested = false;
        }

        lastClickTime = Time.time;

        TryMove();
    }

    //intent mov "inteligent"
    private void TryMove()
    {
        Ray ray = mainCamera.ScreenPointToRay(cursor.GetCursorScreenPosition());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, walkZoneLayer))
        {
            if (NavMesh.SamplePosition(
                hit.point,
                out NavMeshHit navHit,
                navMeshSampleRadius,
                NavMesh.AllAreas))
            {

                agent.ResetPath(); // Reset pk no faci coses rares si ja es mou
                agent.speed = runRequested ? runSpeed : walkSpeed;
                agent.SetDestination(navHit.position);
            }
        }
    }
}
