using UnityEngine;

public class ElectricalBox : Interactable
{
    [SerializeField] private ItemData batteryItem;
    [SerializeField] private int requiredBatteries = 3;

    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private GameObject door;

    private int insertedBatteries = 0;

    public override void Interact(PlayerController player)
    {
        // Si tenim item a la mà
        if (InventoryManager.instance.HasItemInHand())
        {
            ItemData item = InventoryManager.instance.GetItemInHand();

            if (item == batteryItem)
            {
                insertedBatteries++;

                InventoryManager.instance.RemoveItemInHand();

                // Si hem completat el puzzle
                if (insertedBatteries >= requiredBatteries)
                {
                    Destroy(door);

                    dialogueUI.SetPlayer(player);
                    dialogueUI.ShowLine("Parece que se ha abierto algo.");

                    return;
                }

                return;
            }
        }

        player.StopMovement();
        dialogueUI.SetPlayer(player);

        // Cap pila encara
        if (insertedBatteries == 0)
        {
            dialogueUI.ShowLine("Esta caja eléctrica debe servir para algo, pero le faltan piezas.");
            return;
        }

        // Falten piles
        int remaining = requiredBatteries - insertedBatteries;

        if (remaining == 1)
            dialogueUI.ShowLine("Falta 1 pila.");
        else
            dialogueUI.ShowLine("Faltan " + remaining + " pilas.");
    }
}