using UnityEngine;

public class OpenCloset : Interactable
{
    [SerializeField] 
    private Animator closet;
    [SerializeField] 
    private Animator wood;

    private bool used = false;

    public override void Interact(PlayerController player)
    {
        if (used)
        {
            return;
        }
            
        if (closet != null)
        {
            closet.SetTrigger("Abrir");
        }

        if (wood != null)
        {
            wood.SetTrigger("Fusta");
        }
        used = true;
    }
}
