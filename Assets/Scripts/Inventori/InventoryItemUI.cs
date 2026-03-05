using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerClickHandler
{
    private ItemData item;

    private RectTransform rectTransform;
    private Transform slot;

    private bool selected;

    private Vector3 startPosition;

    private VirtualCursorController cursor;

    public void Setup(ItemData data, Transform parentSlot)
    {
        item = data;
        slot = parentSlot;

        rectTransform = GetComponentInParent<RectTransform>();

        Image icon = transform.GetComponent<Image>();
        icon.sprite = item.icon;

        cursor = FindFirstObjectByType<VirtualCursorController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLICK INVENTORY ITEM");

        if (!selected)
        {
            Debug.Log("SELECT ITEM");

            startPosition = rectTransform.position;

            rectTransform.SetParent(slot.root);
            rectTransform.SetAsLastSibling();

            selected = true;

            InventoryManager.instance.SelectItem(item);
        }
        else
        {
            Debug.Log("DESELECT ITEM");

            rectTransform.SetParent(slot);
            rectTransform.position = startPosition;

            selected = false;

            InventoryManager.instance.ClearSelection();
        }
    }

    void Update()
    {
        if (selected)
        {
            rectTransform.position = cursor.GetCursorScreenPosition();
        }
    }
}
