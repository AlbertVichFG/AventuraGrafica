using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private bool movemntLockedOnDialogue = false; // bloquejar moviment durant dialogo


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
    private Animator animator;

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
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Si estem caminant cap a un NPC comprovar arribada
        if (targetNPC != null)
        {
            CheckNPCArrival();
        }

        HandleInput();
        HandleAnimations();
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

        if (!click)
            return;

        // intentar click UI primer
        if (TryClickUI())
            return;

        runOrder = (Time.time - lastClickTime <= doubleClickTime);
        lastClickTime = Time.time;

        TryMoveOrInteract();
    }

    //intent mov "inteligent"
    private void TryMoveOrInteract()
    {
        Ray ray = mainCamera.ScreenPointToRay(cursor.GetCursorScreenPosition());

        // Si tenim un objecte seleccionat de l'inventari
        if (InventoryManager.instance.SelectedItem != null)
        {
            Debug.Log("Using item: " + InventoryManager.instance.SelectedItem.itemName);

            InventoryManager.instance.ClearSelection();
            return;
        }



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

            ItemPickuo pickup = hit.collider.GetComponent<ItemPickuo>();

            if (pickup != null)
            {
                pickup.Interact();
                return;
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

        animator.SetBool("IsWalking", true);
        
        if (NavMesh.SamplePosition(point, out NavMeshHit navHit, navMeshSampleRadius, NavMesh.AllAreas))
        {
            agent.ResetPath();
            agent.speed = runOrder ? runSpeed : walkSpeed;
            agent.SetDestination(navHit.position);

            animator.SetBool("IsWalking", !runOrder);
            animator.SetBool("IsRuning", runOrder);
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

        if (!agent.hasPath || agent.remainingDistance <= interactionDistance)
        {
            agent.ResetPath();
            // Tornar a velocitat normal
            agent.speed = walkSpeed;

            //Player a interacPoint
         //   transform.position = targetPoint.position;
            transform.LookAt(targetNPC.transform); // Mirar al NPC


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

    public void UnlockMovement()
    {
        movementLocked = false;
    }

    // Animacions
    private void HandleAnimations()
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


    bool TryClickUI()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = cursor.GetCursorScreenPosition();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        Debug.Log("UI elements hit: " + results.Count);

        foreach (var r in results)
        {
            Button button = r.gameObject.GetComponent<Button>();

            if (button != null)
            {
                Debug.Log("CLICK UI BUTTON: " + r.gameObject.name);

                button.onClick.Invoke();

                return true; //això evita que el player torni a clicar
            }
        }

        return false;
    }


}
