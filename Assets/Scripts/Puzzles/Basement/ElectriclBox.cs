using UnityEngine;

public class ElectriclBox : Interactable
{
    [Header("Requirements")]
    [SerializeField] private Sprite batteryItem;
    [SerializeField] private int requiredBatteries = 3;

    [Header("Dialogue")]
    [SerializeField] private DialogueUI dialogueUI;


    [TextArea]
    [SerializeField]
    public string needBatteryText = "Falten piles.";

    [SerializeField] private GameObject doorToOpen;

    private int insertedBatteries = 0;

    public override void Interact(PlayerController player)
    {
        if (InventoryManager.instance.HasItemInHand())
        {
            Sprite item = InventoryManager.instance.GetItemInHand();

            if (item == batteryItem)
            {
                insertedBatteries++;

                InventoryManager.instance.RemoveItemInHand();

                if (insertedBatteries >= requiredBatteries)
                {
                    OpenDoor();
                }

                return;
            }
        }

        player.StopMovement();

        dialogueUI.SetPlayer(player);
        dialogueUI.ShowLine(needBatteryText);
    }

    void OpenDoor()
    {
        if (doorToOpen != null)
            doorToOpen.SetActive(false);
    }
}
