using UnityEngine;
using System.Collections;

public class PickupItem : Interactable
{
    [SerializeField]
    private AudioClip sfxItem;
    
    [SerializeField] private ItemData item;
    public override void Interact(PlayerController player)
    {
        player.StartCoroutine(PickupSequence(player));
    }

    IEnumerator PickupSequence(PlayerController player)
    {
        player.StopMovement();

        Animator anim = player.GetComponentInChildren<Animator>();

        if (anim != null)
        {
            anim.SetTrigger("PickItem");

            yield return null;

            float length = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(length);
        }
        else
        {
            Debug.LogWarning("Player Animator no trobat!");
            yield return new WaitForSeconds(0.5f);
        }

        if (item != null)
        {
            InventoryManager.instance.AddItem(item);
        }
        else
        {
            Debug.LogError("ItemData no assignat al PickupItem!");
        }

        player.UnlockMovement();

        Destroy(gameObject);
    }
}