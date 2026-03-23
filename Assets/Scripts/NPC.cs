using UnityEngine;

public class NPC : Interactable
{
    [System.Serializable]
    public class DialoguePhase
    {
        [TextArea(2, 4)]
        public string[] lines;
    }

    [SerializeField]
    private bool affectsGameState = false;

    [Header("Dialogue per Phase")]
    [SerializeField]
    private DialoguePhase[] phases;

    [Header("References")]
    [SerializeField]
    private DialogueUI dialogueUI;

    private bool[] phaseCompleted;

    [Header("Item Puzzle")]
    [SerializeField]
    private ItemData requiredItem;

    [SerializeField]
    private int requiredAmount = 1;

    [SerializeField]
    private int deliveredItems = 0;

    [SerializeField]
    private bool puzzleCompleted = false;

    [Header("Reward")]
    [SerializeField]
    private ItemData rewardItem;

    void Awake()
    {
        phaseCompleted = new bool[phases.Length];
    }

    public override void Interact(PlayerController player)
    {
        if (dialogueUI == null || phases.Length == 0)
            return;

        player.StopMovement();
        dialogueUI.SetPlayer(player);

        // ITEM INTERACTION
        if (InventoryManager.instance != null && InventoryManager.instance.HasItemInHand())
        {
            ItemData item = InventoryManager.instance.GetItemInHand();

            // ITEM CORRECTE
            if (item == requiredItem && !puzzleCompleted)
            {
                deliveredItems++;
                InventoryManager.instance.RemoveItemInHand();

                if (deliveredItems >= requiredAmount)
                {
                    puzzleCompleted = true;

                    if (affectsGameState && GameState.Instance != null)
                    {
                        GameState.Instance.currentPuzzlePhase++;
                    }

                    if (rewardItem != null)
                    {
                        InventoryManager.instance.AddItem(rewardItem);
                    }
                }

                return;
            }
            else
            {
                return;
            }
        }

        // NORMAL DIALOGUE
        int phase = 0;

        if (GameState.Instance != null)
        {
            phase = GameState.Instance.currentPuzzlePhase;
        }

        // Evitar index out of range
        if (phase >= phases.Length)
        {
            phase = phases.Length - 1;
        }

        if (phase < 0)
        {
            phase = 0;
        }

        string[] lines = phases[phase].lines;

        if (lines == null || lines.Length == 0)
            return;

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