using UnityEngine;

public class PickupItem : Interactable
{

    [SerializeField] private ItemData item;

    public override void Interact(PlayerController player)
    {
        InventoryManager.instance.AddItem(item);
        Destroy(gameObject);
    }
}