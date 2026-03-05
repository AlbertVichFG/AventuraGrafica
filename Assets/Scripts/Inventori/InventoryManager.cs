using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventorySlot[] slots;
    public Image cursorItemIcon;
    public VirtualCursorController cursor;

    private Sprite itemInHand;
    private InventorySlot originalSlot;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (itemInHand != null)
        {
            cursorItemIcon.transform.position = cursor.GetCursorScreenPosition();
        }
    }

    public void AddItem(Sprite item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.currentItem == null)
            {
                slot.SetItem(item);
                return;
            }
        }
    }

    public void ClickSlot(InventorySlot slot)
    {
        // Agafar item
        if (itemInHand == null && slot.currentItem != null)
        {
            itemInHand = slot.currentItem;
            originalSlot = slot;

            cursorItemIcon.sprite = itemInHand;
            cursorItemIcon.enabled = true;

            slot.SetItem(null);
        }

        // Deixar item en un altre slot
        else if (itemInHand != null)
        {
            slot.SetItem(itemInHand);

            itemInHand = null;
            originalSlot = null;

            cursorItemIcon.enabled = false;
        }
    }

    public void CancelItemUse()
    {
        if (itemInHand != null && originalSlot != null)
        {
            originalSlot.SetItem(itemInHand);

            itemInHand = null;
            originalSlot = null;

            cursorItemIcon.enabled = false;
        }
    }

    public void ReturnItemToInventory()
    {
        if (itemInHand != null && originalSlot != null)
        {
            originalSlot.SetItem(itemInHand);

            itemInHand = null;
            originalSlot = null;

            cursorItemIcon.enabled = false;
        }
    }

    public bool HasItemInHand()
    {
        return itemInHand != null;
    }

    public Sprite GetItemInHand()
    {
        return itemInHand;
    }
}
