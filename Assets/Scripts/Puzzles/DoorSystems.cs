using UnityEngine;
using UnityEngine.AI;


public class DoorSystem : Interactable
{
    [Header("Key")]
    [SerializeField] 
    private ItemData requiredKey;

    [Header("Dialogue")]
    [SerializeField] 
    private DialogueUI dialogueUI;

    [TextArea]
    [SerializeField] 
    private string lockedLine;

    [Header("Components")]
    [SerializeField] 
    private Animator animator;

    private NavMeshObstacle navMeshObstacle;
    private Collider doorCollider;
    private bool opened = false;

    [SerializeField]
    private GameObject triggerTalk;

    void Awake()
    {
        navMeshObstacle = GetComponentInChildren<NavMeshObstacle>(true);
        doorCollider = GetComponent<Collider>();
    }

    public override void Interact(PlayerController player)
    {
        //player.StopMovement();
        dialogueUI.SetPlayer(player);

        if (opened)
        {
            return;
        }

        if (InventoryManager.instance.HasItemInHand())
        {
            ItemData item = InventoryManager.instance.GetItemInHand();

            if (item == requiredKey)
            {
                InventoryManager.instance.RemoveItemInHand();

                OpenDoor();
                return;
            }
        }

        // Porta tancada
        if (!string.IsNullOrEmpty(lockedLine))
        {
            dialogueUI.ShowLine(lockedLine);
        }
    }

    void OpenDoor()
    {
        opened = true;

        if (navMeshObstacle != null)
        {
            navMeshObstacle.carving = false;
            navMeshObstacle.enabled = false;
        }

        if (doorCollider != null)
            doorCollider.enabled = false;
        
        if (animator != null)
        {
            animator.SetTrigger("PuertaAbierta"); 
            triggerTalk.SetActive(false);
        }
    }
}