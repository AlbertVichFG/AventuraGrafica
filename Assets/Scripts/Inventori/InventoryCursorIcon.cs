using UnityEngine;
using UnityEngine.UI;

public class InventoryCursorIcon : MonoBehaviour
{
    void Awake()
    {

    }

    void Start()
    {
        if (InventoryManager.instance != null)
        {
            InventoryManager.instance.cursorItemIcon = GetComponent<Image>();
            InventoryManager.instance.cursorItemIcon.enabled = false;
        }
    }

}
