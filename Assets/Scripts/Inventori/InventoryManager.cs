using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventorySlot[] slots;
    public Image cursorItemIcon;
    public VirtualCursorController cursor;

    private ItemData itemInHand;
    private InventorySlot originalSlot;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (itemInHand != null)
        {
            cursorItemIcon.transform.position = cursor.GetCursorScreenPosition();
        }
    }

    public void AddItem(ItemData item)
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

            cursorItemIcon.sprite = itemInHand.icon;
            cursorItemIcon.enabled = true;

            slot.SetItem(null);
        }

        // Deixar item
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

    public ItemData GetItemInHand()
    {
        return itemInHand;
    }

    public void RemoveItemInHand()
    {
        itemInHand = null;
        cursorItemIcon.enabled = false;
    }
}