using UnityEngine;

public class PalancaPuzzle : Interactable
{
    [Header("Animacions")]
    [SerializeField] 
    private Animator leverAnimator;
    [SerializeField] 
    private Animator doorAnimator;

    [Header("NPC")]
    [SerializeField] 
    private GameObject npcToActivate;

    [Header("Book")]
    [SerializeField]
    private GameObject bookToActivate;

    [Header("Opcions")]
    [SerializeField] 
    private bool disableAfterUse = true;

    private bool activated = false;

    public override void Interact(PlayerController player)
    {
        if (activated)
        {
            return;
        }      

        activated = true;

        // animaci� palanca
        if (leverAnimator != null)
        {
            leverAnimator.SetTrigger("On");
        }
            
        // activar llibre
        if (bookToActivate != null)
        {
            bookToActivate.SetActive(true);
        }
            
        // animaci� porta
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("armariosAbrir");
        }

        // activar npc
        if (npcToActivate != null)
        {
            npcToActivate.SetActive(true);
        }
            
        // opcional: desactivar palanca
        if (disableAfterUse)
        {
            GetComponent<Collider>().enabled = false;
        }      
    }
}
