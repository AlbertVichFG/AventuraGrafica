using System.Collections;
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

    // Lista maestra de items en memoria, independiente de los slots
    private List<ItemData> currentItems = new List<ItemData>();

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
            cursorItemIcon.transform.position = cursor.GetCursorScreenPosition();
    }

    public void RegisterSlot(InventorySlot slot)
    {
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
        // Guardar en lista maestra
        currentItems.Add(item);

        // Mostrar en slot
        foreach (InventorySlot slot in slots)
        {
            if (slot.currentItem == null)
            {
                slot.SetItem(item);
                return;
            }
        }
    }

    public void RemoveItem(ItemData item)
    {
        currentItems.Remove(item);

        foreach (InventorySlot slot in slots)
        {
            if (slot.currentItem == item)
            {
                slot.SetItem(null);
                return;
            }
        }
    }

    public void ClickSlot(InventorySlot slot)
    {
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

    public bool HasItemInHand() => itemInHand != null;
    public ItemData GetItemInHand() => itemInHand;

    public void RemoveItemInHand()
    {
        if (itemInHand != null)
        {
            currentItems.Remove(itemInHand);
            itemInHand = null;
        }

        if (cursorItemIcon != null)
            cursorItemIcon.enabled = false;
    }

    public void PopulateSaveData(GameData data)
    {
        data.inventoryItems.Clear();
        foreach (ItemData item in currentItems)
        {
            if (item != null)
                data.inventoryItems.Add(item.itemName);
        }
    }

    public void LoadFromSaveData(GameData data)
    {
        currentItems.Clear();

        foreach (InventorySlot slot in slots)
            if (slot != null) slot.SetItem(null);

        ItemData[] allItems = Resources.LoadAll<ItemData>("Items");

        foreach (string itemName in data.inventoryItems)
        {
            ItemData found = System.Array.Find(allItems, i => i.itemName == itemName);
            if (found != null)
                AddItem(found);
            else
                Debug.LogWarning($"[InventoryManager] No se encontró el item: {itemName}");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu") return;

        slots.Clear();

        InventoryCursorIcon icon = FindFirstObjectByType<InventoryCursorIcon>();
        if (icon != null)
            cursorItemIcon = icon.GetComponent<Image>();

        VirtualCursorController cur = FindFirstObjectByType<VirtualCursorController>();
        if (cur != null)
            cursor = cur;

        StartCoroutine(RestoreItemsNextFrame());
    }

    private IEnumerator RestoreItemsNextFrame()
    {
        yield return new WaitUntil(() => slots.Count > 0);

        foreach (InventorySlot slot in slots)
            if (slot != null) slot.SetItem(null);

        foreach (ItemData item in currentItems)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.currentItem == null)
                {
                    slot.SetItem(item);
                    break;
                }
            }
        }
    }
}