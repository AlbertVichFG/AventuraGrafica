using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    [Header("References")]
    [SerializeField] 
    private VirtualCursorController cursor;

    [Header("Movement")]
    [SerializeField] 
    private float walkSpeed = 3.5f;
    [SerializeField] 
    private float runSpeed = 6f;
    [SerializeField] 
    private float doubleClickTime = 0.3f;

    [Header("Navigation")]
    [SerializeField] 
    private float navMeshSampleRadius = 1f;
    [SerializeField] 
    private float interactionDistance = 1.2f;

    [Header("Layers")]
    [SerializeField] 
    private LayerMask walkZoneLayer;
    [SerializeField] 
    private LayerMask interactableLayer;
    [SerializeField] 
    private LayerMask obstacleLayer;

    public bool ignoreNavMeshSnap = false;

    private NavMeshAgent agent;
    private Camera mainCamera;
    private Animator animator;

    private float lastClickTime;
    private bool runOrder;
    private bool movementLocked = false;

    private Interactable targetInteractable;
    private Transform targetPoint;

    [SerializeField]
    private AudioClip sfxWalk;
    private float lastStepTime;
    private float stepCooldown = 0.5f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

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
        if (ignoreNavMeshSnap)
        {
            return;
        }
            
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 0.3f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
    }

    void HandleInput()
    {
        if (movementLocked)
        {
            return;
        }

        bool mouseClick = Mouse.current.leftButton.wasPressedThisFrame;
        bool gamepadClick = Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame;

        if (!mouseClick && !gamepadClick)
        {
            return;
        }

        // SI EL CLICK ee DEL MANDO  provar UI manualment
        if (gamepadClick)
        {
            if (ClickUI())
            {
                return;
            }
        }
        //mouse utilitza el sistema normal de Unity UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
            
        //InventoryManager.instance.CancelItemUse();

        runOrder = (Time.time - lastClickTime <= doubleClickTime);
        lastClickTime = Time.time;

        TryMoveOrInteract();
    }

    void TryMoveOrInteract()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
            
        Ray ray = mainCamera.ScreenPointToRay(cursor.GetCursorScreenPosition());
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (var hit in hits)
        {
            int layer = hit.collider.gameObject.layer;
           // Debug.Log(hit.transform.name);
            // si es paret bloquejar
            if (((1 << layer) & obstacleLayer) != 0)
            {
              //  Debug.Log("Bloquejat per paret: " + hit.collider.name);
                return;
            }

            // interactables
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                StartInteraction(interactable);
                return;
            }

            // caminar
            if (((1 << layer) & walkZoneLayer) != 0)
            {
              //  Debug.Log("Entro caminar");
                MoveToPoint(hit.point);
                return;
            }
        }
    }

    void StartInteraction(Interactable interactable)
    {
        if (TamboretPuzzle.playerOnStool)
        {
            interactable.Interact(this);
            return;
        }

        targetInteractable = interactable;
        targetPoint = interactable.transform.Find("InteractionPoint");

        if (targetPoint == null)
        {
            targetPoint = interactable.transform;
        }

        agent.ResetPath();
        agent.speed = runOrder ? runSpeed : walkSpeed;
        agent.SetDestination(targetPoint.position);
    }

    void CheckInteractionArrival()
    {
        if (agent.pathPending)
        {
            return;
        }

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
        agent.isStopped = true;

    }

    public void UnlockMovement()
    {
        movementLocked = false;
        agent.isStopped = false;
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
            if (Time.time - lastStepTime > 0.5f)
            {
                if (AudioManager.instance != null && sfxWalk != null)
                {
                    AudioManager.instance.PlaySFX(sfxWalk, transform.position);
                    lastStepTime = Time.time;
                }
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRuning", false);
        }
    }

    //camera
    public void SetActiveCamera(Camera cam)
    {
        mainCamera = cam;
    }

    bool ClickUI()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = cursor.GetCursorScreenPosition();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
            ExecuteEvents.Execute(results[0].gameObject, pointerData, ExecuteEvents.pointerClickHandler);
            return true;
        }

        return false;
    }

    public void FinishPickup()
    {
        UnlockMovement();
    }
}