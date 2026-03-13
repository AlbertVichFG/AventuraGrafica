using UnityEngine;

public class Glue : Interactable
{
    [SerializeField] private ItemData glueItem;
    [SerializeField] private DialogueUI dialogueUI;

    [TextArea]
    [SerializeField] private string needSomethingLine;

    [TextArea]
    [SerializeField] private string canReachLine;

    public override void Interact(PlayerController player)
    {
        dialogueUI.SetPlayer(player);

        if (!TamboretSpot.stoolPlaced)
        {
            dialogueUI.ShowLine(needSomethingLine);
            return;
        }

        dialogueUI.ShowLine(canReachLine);

        InventoryManager.instance.AddItem(glueItem);

        Destroy(gameObject);
    }
}
