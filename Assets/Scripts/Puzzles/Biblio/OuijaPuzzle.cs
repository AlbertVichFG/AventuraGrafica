using UnityEngine;

public class OuijaPuzzle : Interactable
{
    [Header("Item necessari")]
    [SerializeField] private ItemData requiredItem;

    [Header("Animaci�")]
    [SerializeField] 
    private Animator ouijaAnimator;

    [Header("Objecte")]
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private GameObject objectToDesactive;

    [Header("Dialogue")]
    [SerializeField] private DialogueUI dialogueUI;

    [TextArea]
    [SerializeField] private string noItemLine;

    [TextArea]
    [SerializeField] private string wrongItemLine;

    private bool solved = false;

    public override void Interact(PlayerController player)
    {
        dialogueUI.SetPlayer(player);

        if (solved)
        {
            return;
        }
            
        // sense item
        if (!InventoryManager.instance.HasItemInHand())
        {
            dialogueUI.ShowLine(noItemLine);
            return;
        }

        ItemData item = InventoryManager.instance.GetItemInHand();

        // item incorrecte
        if (item != requiredItem)
        {
            dialogueUI.ShowLine(wrongItemLine);
            return;
        }

        // item correcte
        InventoryManager.instance.RemoveItemInHand();
        objectToDesactive.SetActive(false);

        if (ouijaAnimator != null)
        {
            ouijaAnimator.SetTrigger("Ouija");
        }
            
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }    

        solved = true;
    }
}

