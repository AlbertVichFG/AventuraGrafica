using System.Collections;
using UnityEngine;

public class PickupItem : Interactable
{
    [SerializeField]
    private AudioClip sfxItem;
    
    [SerializeField] 
    private ItemData item;

    private PlayerController currentPlayer;
    public override void Interact(PlayerController player)
    {
        player.StartCoroutine(PickupSequence(player));
    }

    IEnumerator PickupSequence(PlayerController player)
    {
        player.StopMovement();

        Animator anim = player.GetComponentInChildren<Animator>();

        anim.SetTrigger("PickItem");

        if (AudioManager.instance != null && sfxItem != null)
        {
            AudioManager.instance.PlaySFX(sfxItem, transform.position);
        }

        yield return null;
        float length = anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(length);

        InventoryManager.instance.AddItem(item);

        player.UnlockMovement();

        Destroy(gameObject);
    }

    public void FinishPickup()
    {
        Debug.Log("Finish pickup called");
        InventoryManager.instance.AddItem(item);

        currentPlayer.UnlockMovement();

        Destroy(gameObject);
    }
}