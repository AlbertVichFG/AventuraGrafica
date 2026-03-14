using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<InventorySlot> slots = new List<InventorySlot>();
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
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (itemInHand != null && cursorItemIcon != null && cursor != null)
        {
            cursorItemIcon.transform.position = cursor.GetCursorScreenPosition();
        }
    }

    public void RegisterSlot(InventorySlot slot)
    {
        // eliminar referències destruïdes
        slots.RemoveAll(s => s == null);

        if (!slots.Contains(slot))
        {
            slots.Add(slot);

            slots.Sort((a, b) =>
                a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
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
        // AGAFAR ITEM
        if (itemInHand == null && slot.currentItem != null)
        {
            itemInHand = slot.currentItem;
            originalSlot = slot;

            if (cursorItemIcon != null)
            {
                cursorItemIcon.sprite = itemInHand.icon;
                cursorItemIcon.enabled = true;
            }

            slot.SetItem(null);
            return;
        }

        // DEIXAR ITEM
        if (itemInHand != null)
        {
            if (slot.currentItem == null)
            {
                slot.SetItem(itemInHand);
            }
            else
            {
                ItemData temp = slot.currentItem;
                slot.SetItem(itemInHand);

                if (originalSlot != null)
                    originalSlot.SetItem(temp);
            }

            itemInHand = null;
            originalSlot = null;

            if (cursorItemIcon != null)
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

            if (cursorItemIcon != null)
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

        if (cursorItemIcon != null)
            cursorItemIcon.enabled = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // buidem slots antics
        slots.Clear();

        // busquem el cursor icon del nou canvas
      //  cursorItemIcon = GameObject.Find("CursorItemIcon")?.GetComponent<Image>();
    }
}