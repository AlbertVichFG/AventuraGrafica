using UnityEngine;

public class NPC : Interactable
{
    [System.Serializable]
    public class DialoguePhase
    {
        [TextArea(2, 4)]
        public string[] lines;
    }

    [SerializeField] private bool affectsGameState = false;

    [Header("Dialogue per Phase")]
    [SerializeField] private DialoguePhase[] phases;

    [Header("References")]
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private bool[] phaseCompleted;

    [Header("Item Puzzle")]
    [SerializeField] private ItemData requiredItem;
    [SerializeField] private int requiredAmount = 1;
    [SerializeField] private int deliveredItems = 0;
    [SerializeField] private bool puzzleCompleted = false;
    [SerializeField] private bool firstTalkDone = false;

    [Header("Reward")]
    [SerializeField] private ItemData rewardItem;

    [Header("Dialogue Lines")]
    [TextArea(2, 3)]
    [SerializeField] private string progressLine;

    [TextArea(2, 3)]
    [SerializeField] private string wrongItemLine;

    [TextArea(2, 3)]
    [SerializeField] private string puzzleCompletedLine;

    [TextArea(2, 3)]
    [SerializeField] private string puzzleSolvedLine;

    private int[] phaseIndexes;

    void Awake()
    {
        phaseIndexes = new int[phases.Length];
        phaseCompleted = new bool[phases.Length];
    }

    public override void Interact(PlayerController player)
    {
        player.StopMovement();
        dialogueUI.SetPlayer(player);

        // ITEM INTERACTION

        if (InventoryManager.instance.HasItemInHand())
        {
            ItemData item = InventoryManager.instance.GetItemInHand();

            // PUZZLE JA COMPLETAT
            if (puzzleCompleted)
            {
                dialogueUI.ShowLine(puzzleCompletedLine);
                return;
            }

            // ITEM CORRECTE
            if (item == requiredItem)
            {
                deliveredItems++;

                InventoryManager.instance.RemoveItemInHand();

                dialogueUI.ShowLine(progressLine + " (" + deliveredItems + "/" + requiredAmount + ")");

                if (deliveredItems >= requiredAmount)
                {
                    puzzleCompleted = true;

                    if (affectsGameState)
                    {
                        GameState.Instance.currentPuzzlePhase++;
                    }

                    if (rewardItem != null)
                    {
                        InventoryManager.instance.AddItem(rewardItem);
                    }

                    dialogueUI.ShowLine(puzzleSolvedLine);
                }

                return;
            }
            else
            {
                dialogueUI.ShowLine(wrongItemLine);
                return;
            }
        }

        // FIRST TALK TRIGGER

        if (affectsGameState && !firstTalkDone && GameState.Instance.currentPuzzlePhase == 0)
        {
            firstTalkDone = true;
            GameState.Instance.currentPuzzlePhase = 1;
        }

        // NORMAL DIALOGUE

        int phase = GameState.Instance.currentPuzzlePhase;

        if (phase < 0 || phase >= phases.Length)
            return;

        string[] lines = phases[phase].lines;

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