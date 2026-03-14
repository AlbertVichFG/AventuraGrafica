using UnityEngine;

public class TamboretSpot : Interactable
{
    [SerializeField] private ItemData stoolItem;
    [SerializeField] private GameObject stoolVisual;

    public static bool stoolPlaced = false;

    public override void Interact(PlayerController player)
    {
        Debug.Log("Intentant posar tamboret");

        if (!InventoryManager.instance.HasItemInHand())
            return;

        ItemData item = InventoryManager.instance.GetItemInHand();

        if (item == stoolItem && !stoolPlaced)
        {
            InventoryManager.instance.RemoveItemInHand();

            stoolVisual.SetActive(true);

            stoolPlaced = true;
        }
    }
}
