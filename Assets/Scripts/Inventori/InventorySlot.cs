using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Sprite currentItem;

    public void SetItem(Sprite itemSprite)
    {
        currentItem = itemSprite;

        if (itemSprite == null)
        {
            icon.enabled = false;
        }
        else
        {
            icon.sprite = itemSprite;
            icon.enabled = true;
        }
    }


        public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Slot clicked");

        InventoryManager.instance.ClickSlot(this);
    }

}