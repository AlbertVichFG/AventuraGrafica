using UnityEngine;

public class ItemPickuo : MonoBehaviour
{
    [SerializeField] private ItemData item;


    public void Interact()
    {
        Debug.Log("ITEM PICKED");

        InventoryManager.instance.AddItem(item);

        Destroy(gameObject);
    }
}
