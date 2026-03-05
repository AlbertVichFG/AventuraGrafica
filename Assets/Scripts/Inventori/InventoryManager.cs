using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [SerializeField] private InventoryUI ui;

    private List<ItemData> items = new List<ItemData>();

    public ItemData SelectedItem { get; private set; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemData item)
    {
        ui.AddItem(item);
    }

    public void SelectItem(ItemData item)
    {
        SelectedItem = item;
 
    }

    public void ClearSelection()
    {
        SelectedItem = null;
    }

}
