using System.Collections;
using UnityEngine;

public class PickupItem : Interactable
{
    [SerializeField]
    private AudioClip sfxItem;

    [SerializeField]
    private ItemData item;

    private PlayerController currentPlayer;

    void Start()
    {
        StartCoroutine(CheckIfPickedUp());
    }

    private IEnumerator CheckIfPickedUp()
    {
        yield return null;
        if (GameManager.instance != null && GameManager.instance.IsItemPickedUp(GetItemKey()))
            Destroy(gameObject);
    }

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

            if (AudioManager.instance != null && sfxItem != null)
                AudioManager.instance.PlaySFX(sfxItem, transform.position);

            yield return null;
            float length = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(length);
        }
        else
        {
            if (AudioManager.instance != null && sfxItem != null)
                AudioManager.instance.PlaySFX(sfxItem, transform.position);

            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(4f);

        if (GameManager.instance != null)
            GameManager.instance.RegisterPickedUpItem(GetItemKey());

        if (InventoryManager.instance != null)
            InventoryManager.instance.AddItem(item);
        else
            Debug.LogWarning("[PickupItem] InventoryManager.instance es null");

        player.UnlockMovement();
        Destroy(gameObject);
    }

    public void FinishPickup()
    {
        Debug.Log("Finish pickup called");
        if (GameManager.instance != null)
            GameManager.instance.RegisterPickedUpItem(GetItemKey());
        if (InventoryManager.instance != null)
            InventoryManager.instance.AddItem(item);
        if (currentPlayer != null)
            currentPlayer.UnlockMovement();
        Destroy(gameObject);
    }

    private string GetItemKey()
    {
        return $"{UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}/{gameObject.name}";
    }
}