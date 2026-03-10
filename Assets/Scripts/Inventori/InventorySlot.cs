using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public ItemData currentItem;

    public void SetItem(ItemData item)
    {
        currentItem = item;

        if (item == null)
        {
            icon.enabled = false;
        }
        else
        {
            icon.sprite = item.icon;
            icon.enabled = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.instance.ClickSlot(this);
    }

}