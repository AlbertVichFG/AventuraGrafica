using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Sprite itemIcon;

    public void Pick()
    {
        InventoryManager.instance.AddItem(itemIcon);

        Destroy(gameObject);
    }
}