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
        // Si este item ya fue recogido en una partida anterior, destruirlo
        string key = GetItemKey();
        if (GameManager.instance.IsItemPickedUp(key))
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
        anim.SetTrigger("PickItem");

        if (AudioManager.instance != null && sfxItem != null)
            AudioManager.instance.PlaySFX(sfxItem, transform.position);

        yield return null;
        float length = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);

        // Registrar como recogido
        GameManager.instance.RegisterPickedUpItem(GetItemKey());
        InventoryManager.instance.AddItem(item);

        player.UnlockMovement();
        Destroy(gameObject);
    }

    public void FinishPickup()
    {
        Debug.Log("Finish pickup called");
        GameManager.instance.RegisterPickedUpItem(GetItemKey());
        InventoryManager.instance.AddItem(item);
        currentPlayer.UnlockMovement();
        Destroy(gameObject);
    }

    private string GetItemKey()
    {
        return $"{UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}/{gameObject.name}";
    }
}