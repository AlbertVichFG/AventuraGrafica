using UnityEngine;
using System.Collections;


public class BathtubPuzzle : Interactable
{
    [Header("Item correcte")]
    [SerializeField] 
    private ItemData requiredItem;

    [Header("Animaci� martell")]
    [SerializeField] 
    private Animator hammerAnimator;

    [Header("Collider banyera")]
    [SerializeField] 
    private Collider bathtubCollider;

    [Header("Dialogue")]
    [SerializeField] 
    private DialogueUI dialogueUI;

    [TextArea]
    [SerializeField] 
    private string noItemLine;

    [TextArea]
    [SerializeField] 
    private string wrongItemLine;

    private bool solved = false;

    public override void Interact(PlayerController player)
    {
      //  Debug.Log("BATHTUB INTERACTUAR");
        dialogueUI.SetPlayer(player);

        if (solved)
        {
    //        Debug.Log("Puzzle ja resolt");
            return;
        }

        if (!InventoryManager.instance.HasItemInHand())
        {
     //       Debug.Log("NO ITEM");
            dialogueUI.ShowLine(noItemLine);
            return;
        }

        ItemData item = InventoryManager.instance.GetItemInHand();
      //  Debug.Log("Item a la ma: " + item.name);
      //  Debug.Log("Item requerit: " + requiredItem.name);

      //  Debug.Log("ITEM CORRECTE resolent puzzle");

        StartCoroutine(SolvePuzzle());
    }

    IEnumerator SolvePuzzle()
    {
        solved = true;
        Debug.Log("Eliminant item inventari");
        InventoryManager.instance.RemoveItemInHand();
        Debug.Log("Animaci� martell");

        if (hammerAnimator != null)
        {
            hammerAnimator.SetBool("Flota", true);
        }
        else
        {
            Debug.LogError("hammerAnimator NULL");
        }

        yield return new WaitForSeconds(1.2f);

        Debug.Log("Desactivant collider banyera");

        if (bathtubCollider != null)
        {
            bathtubCollider.enabled = false;
        }
        else
        {
            Debug.LogError("bathtubCollider NULL");
        }
    }
}

