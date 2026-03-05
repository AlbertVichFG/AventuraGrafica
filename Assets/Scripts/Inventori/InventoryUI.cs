using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform[] slots;
    [SerializeField] private GameObject itemPrefab;

    private RectTransform draggedItem;

    public void AddItem(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].childCount == 0)
            {
                GameObject obj = Instantiate(itemPrefab, slots[i]);

                InventoryItemUI uiItem = obj.GetComponentInChildren<InventoryItemUI>();
                uiItem.Setup(item, slots[i]);

                break;
            }
        }
    }

}
