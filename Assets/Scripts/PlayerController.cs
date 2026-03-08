using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private VirtualCursorController cursor;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float doubleClickTime = 0.3f;

    [Header("Navigation")]
    [SerializeField] private float navMeshSampleRadius = 1f;
    [SerializeField] private float interactionDistance = 1.2f;

    [Header("Layers")]
    [SerializeField] private LayerMask walkZoneLayer;
    [SerializeField] private LayerMask interactableLayer;

    private NavMeshAgent agent;
    private Camera mainCamera;
    private Animator animator;

    private float lastClickTime;
    private bool runOrder;
    private bool movementLocked = false;

    private Interactable targetInteractable;
    private Transform targetPoint;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();

        agent.speed = walkSpeed;
    }

    void Update()
    {
        if (targetInteractable != null)
        {
            CheckInteractionArrival();
        }

        HandleInput();
        HandleAnimations();
    }

    void LateUpdate()
    {
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 0.3f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
    }

    void HandleInput()
    {
        if (movementLocked || InventoryManager.instance.HasItemInHand())
            return;

        bool click =
            Mouse.current.leftButton.wasPressedThisFrame ||
            (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame);

        if (!click)
            return;

        InventoryManager.instance.CancelItemUse();

        runOrder = (Time.time - lastClickTime <= doubleClickTime);
        lastClickTime = Time.time;

        TryMoveOrInteract();
    }

    void TryMoveOrInteract()
    {
        Ray ray = mainCamera.ScreenPointToRay(cursor.GetCursorScreenPosition());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer))
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                StartInteraction(interactable);
                return;
            }
        }

        if (Physics.Raycast(ray, out hit, 100f, walkZoneLayer))
        {
            if (InventoryManager.instance.HasItemInHand())
            {
                InventoryManager.instance.ReturnItemToInventory();
                return;
            }

            MoveToPoint(hit.point);
        }
    }

    void StartInteraction(Interactable interactable)
    {
        targetInteractable = interactable;

        targetPoint = interactable.transform.Find("InteractionPoint");

        if (targetPoint == null)
            targetPoint = interactable.transform;

        agent.ResetPath();
        agent.speed = runOrder ? runSpeed : walkSpeed;
        agent.SetDestination(targetPoint.position);
    }

    void CheckInteractionArrival()
    {
        if (agent.pathPending)
            return;

        if (!agent.hasPath || agent.remainingDistance <= interactionDistance)
        {
            agent.ResetPath();

            transform.LookAt(targetInteractable.transform);

            targetInteractable.Interact(this);

            targetInteractable = null;
            targetPoint = null;
        }
    }

    void MoveToPoint(Vector3 point)
    {
        targetInteractable = null;

        if (NavMesh.SamplePosition(point, out NavMeshHit navHit, navMeshSampleRadius, NavMesh.AllAreas))
        {
            agent.ResetPath();
            agent.speed = runOrder ? runSpeed : walkSpeed;
            agent.SetDestination(navHit.position);

            animator.SetBool("IsWalking", !runOrder);
            animator.SetBool("IsRuning", runOrder);
        }
    }

    public void StopMovement()
    {
        movementLocked = true;
        agent.ResetPath();
    }

    public void UnlockMovement()
    {
        movementLocked = false;
    }

    void HandleAnimations()
    {
        float speed = agent.velocity.magnitude;

        if (speed > 0.1f)
        {
            if (agent.speed == runSpeed)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRuning", true);
            }
            else
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRuning", false);
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRuning", false);
        }
    }
}