using UnityEngine;
using System.Collections;

public class PickupItem : Interactable
{

    [SerializeField] private ItemData item;
    public override void Interact(PlayerController player)
    {
        player.StartCoroutine(PickupSequence(player));
    }

    IEnumerator PickupSequence(PlayerController player)
    {
        player.StopMovement();

        Animator anim = player.GetComponentInChildren<Animator>();

        anim.SetTrigger("PickItem");

        yield return null;

        float length = anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(length);

        InventoryManager.instance.AddItem(item);

        player.UnlockMovement();

        Destroy(gameObject);
    }
}