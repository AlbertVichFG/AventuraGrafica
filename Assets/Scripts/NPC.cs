using UnityEngine;

public class NPC : Interactable
{
    [System.Serializable]
    public class DialoguePhase
    {
        [TextArea(2, 4)]
        public string[] lines;
    }

    [Header("Dialogue per Phase")]
    [SerializeField]
    private DialoguePhase[] phases;

    [Header("References")]
    [SerializeField]
    private DialogueUI dialogueUI;
    [SerializeField]
    private bool[] phaseCompleted;

    [Header("Item Puzzle")]
    [SerializeField] 
    private ItemData requiredItem;
    [SerializeField] 
    private int requiredAmount;
    [SerializeField]
    private int deliveredItems = 0;

    private int[] phaseIndexes;

    void Awake()
    {
        phaseIndexes = new int[phases.Length];
        phaseCompleted = new bool[phases.Length];

        //   player = FindFirstObjectByType<PlayerController>();
    }

    public override void Interact(PlayerController player)
    {
        // Si el player té item a la mà
        if (InventoryManager.instance.HasItemInHand())
        {
            ItemData item = InventoryManager.instance.GetItemInHand();

            if (item == requiredItem)
            {
                deliveredItems++;

                InventoryManager.instance.RemoveItemInHand();

                player.StopMovement();
                dialogueUI.SetPlayer(player);

                dialogueUI.ShowLine("Gràcies! (" + deliveredItems + "/" + requiredAmount + ")");

                if (deliveredItems >= requiredAmount)
                {
                    GameState.Instance.currentPuzzlePhase++;
                }

                return;
            }
        }

        // DIÀLEG NORMAL
        int phase = GameState.Instance.currentPuzzlePhase;

        if (phase < 0 || phase >= phases.Length)
            return;

        string[] lines = phases[phase].lines;

        player.StopMovement();
        dialogueUI.SetPlayer(player);

        if (!phaseCompleted[phase])
        {
            dialogueUI.ShowDialogue(lines);
            phaseCompleted[phase] = true;
        }
        else
        {
            dialogueUI.ShowLine(lines[lines.Length - 1]);
        }
    }
}