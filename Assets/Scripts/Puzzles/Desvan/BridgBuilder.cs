using UnityEngine;
using UnityEngine.AI;

public class BridgBuilder : Interactable
{
    [Header("Item necessari")]
    [SerializeField] 
    private ItemData woodItem;

    [Header("Parts del pont")]
    [SerializeField] 
    private GameObject[] bridgeParts;

    [Header("Obstacle")]
    [SerializeField] 
    private NavMeshObstacle obstacle;

    private int placedWood = 0;

    public override void Interact(PlayerController player)
    {
        if (!InventoryManager.instance.HasItemInHand())
        {
            return;
        }
            
        ItemData item = InventoryManager.instance.GetItemInHand();

        if (item != woodItem)
        {
            return;
        }
            
        if (placedWood >= bridgeParts.Length)
        {
            return;
        }

        // consumir item
        InventoryManager.instance.RemoveItemInHand();
        // activar part del pont
        bridgeParts[placedWood].SetActive(true);
        placedWood++;

        // si pont complet
        if (placedWood >= bridgeParts.Length)
        {
            if (obstacle != null)
            {
                obstacle.enabled = false;
            }
        }
    }
}
