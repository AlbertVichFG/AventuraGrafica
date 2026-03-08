using UnityEngine;

public class PickupItem : Interactable
{
    [SerializeField] private Sprite itemIcon;

    public override void Interact(PlayerController player)
    {
        InventoryManager.instance.AddItem(itemIcon);

        Destroy(gameObject);
    }
}