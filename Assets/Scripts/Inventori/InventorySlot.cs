using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public ItemData currentItem;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") return;

        if (InventoryManager.instance != null)
            InventoryManager.instance.RegisterSlot(this);
    }

    public void SetItem(ItemData item)
    {
        currentItem = item;

        if (icon == null) return;

        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLICK SLOT: " + name);

        if (InventoryManager.instance != null)
            InventoryManager.instance.ClickSlot(this);
    }
}