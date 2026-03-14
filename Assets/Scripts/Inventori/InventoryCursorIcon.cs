using UnityEngine;
using UnityEngine.UI;

public class InventoryCursorIcon : MonoBehaviour
{
    void Awake()
    {
        /*Image img = GetComponent<Image>();

        if (InventoryManager.instance != null)
        {
            InventoryManager.instance.cursorItemIcon = img;
        }

        // començar ocult
        img.enabled = false;*/

       // InventoryManager.instance.cursorItemIcon = GetComponent<Image>();
       // InventoryManager.instance.cursorItemIcon.enabled = false;
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
