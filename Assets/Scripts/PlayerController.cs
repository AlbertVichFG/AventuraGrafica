using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] 
    private VirtualCursorController cursor;

    [Header("Movement")]
    [SerializeField] 
    private float walkSpeed;
    [SerializeField] 
    private float runSpeed;
    [SerializeField] 
    private float doubleClickTime; 


    [SerializeField] 
    private float navMeshSampleRadius; //evitar soritr fora navmesh


    [SerializeField] 
    private LayerMask walkZoneLayer;
    [SerializeField]
    private LayerMask interactableLayer;

    [SerializeField] 
    private float interactionDistance;




    private NavMeshAgent agent;
    private Camera mainCamera;

    private float lastClickTime;
    private bool runOrder; 
    private bool movementLocked = false;

    // Target NPC
    private NPC targetNPC;
    private Transform targetPoint;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        agent.speed = walkSpeed;
    }

    void Update()
    {
        // Si estem caminant cap a un NPC comprovar arribada
        if (targetNPC != null)
        {
            CheckNPCArrival();
        }

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
        if (movementLocked)
            return;

        bool click =
            Mouse.current.leftButton.wasPressedThisFrame ||
            (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame);

        //si no click res
        if (!click)
            return;

        // doble click run
        runOrder = (Time.time - lastClickTime <= doubleClickTime);
        lastClickTime = Time.time;

        TryMoveOrInteract();
    }

    //intent mov "inteligent"
    private void TryMoveOrInteract()
    {
        Ray ray = mainCamera.ScreenPointToRay(cursor.GetCursorScreenPosition());

        // PRIORITAT NPC Objectes interactuables
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer))
        {
            if (hit.collider.CompareTag("Talk"))
            {
                NPC npc = hit.collider.GetComponentInParent<NPC>();

                if (npc != null)
                {
                    StartNPCInteraction(npc);
                    return;
                }
            }
        }

        //MOVIMENT terra WalkZone
        if (Physics.Raycast(ray, out hit, 100f, walkZoneLayer))
        {
            MoveToPoint(hit.point);
        }
    }

    private void MoveToPoint(Vector3 point)
    {
        CancelNPCInteraction();
        


        if (NavMesh.SamplePosition(point, out NavMeshHit navHit, navMeshSampleRadius, NavMesh.AllAreas))
        {
            agent.ResetPath();
            agent.speed = runOrder ? runSpeed : walkSpeed;
            agent.SetDestination(navHit.position);
        }
    }

    private void StartNPCInteraction(NPC npc) 
    {
        targetNPC = npc;

        // Buscar InteractionPoint dins del NPC
        targetPoint = npc.transform.Find("InteractionPoint");

        if (targetPoint == null)
        {
            Debug.LogWarning("NPC has no InteractionPoint!");
            targetNPC = null;
            return;
        }

        
        agent.ResetPath();
        agent.speed = runOrder ? runSpeed : walkSpeed; // ajustar velocitat
        agent.SetDestination(targetPoint.position);
    }

    private void CheckNPCArrival()
    {
        if (agent.pathPending)
            return;

        if (agent.remainingDistance <= interactionDistance)
        {
            agent.ResetPath();

            // Tornar a velocitat normal
            agent.speed = walkSpeed;

            // Parlar
            targetNPC.Talk();

            // Reset target
            targetNPC = null;
            targetPoint = null;
        }
    }

    private void CancelNPCInteraction()
    {
        targetNPC = null;
        targetPoint = null;
    }

    public void StopMovement()
    {
        movementLocked = true;
        agent.ResetPath();
    }

    public void ResumeMovement()
    {
        movementLocked = false;
    }

}
