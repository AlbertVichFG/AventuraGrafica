using UnityEngine;

public class ElectriclBox : MonoBehaviour
{
    [Header("Requirements")]
    [SerializeField] private Sprite batteryItem;
    [SerializeField] private int batteriesRequired = 3;

    [Header("Door")]
    [SerializeField] private GameObject door;

    private int batteriesInserted = 0;

    public void Interact()
    {
        // Si el player té item a la mà
        if (InventoryManager.instance.HasItemInHand())
        {
            Sprite item = InventoryManager.instance.GetItemInHand();

            if (item == batteryItem)
            {
                batteriesInserted++;

                InventoryManager.instance.RemoveItemInHand();

                Debug.Log("Battery inserted: " + batteriesInserted);

                if (batteriesInserted >= batteriesRequired)
                {
                    OpenDoor();
                }

                return;
            }
        }

        // si no té bateria
        Debug.Log("Fan falta piles.");
    }

    void OpenDoor()
    {
        Debug.Log("Power restored!");

        if (door != null)
            door.SetActive(false);
    }
}
