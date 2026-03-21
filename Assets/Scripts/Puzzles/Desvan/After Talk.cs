using UnityEngine;

public class AfterTalk : Interactable
{
    [Header("Phase requerida")]
    [SerializeField] 
    private int requiredPhase = 1;

    [Header("Animación palanca")]
    [SerializeField] 
    private Animator leverAnimator;

    [Header("Objetos a activar/desactivar")]
    [SerializeField] 
    private GameObject objectToActivate;
    [SerializeField] 
    private GameObject triggerToActivate;
    [SerializeField] 
    private GameObject pared; // objeto a desactivar

    private bool used = false;

    public override void Interact(PlayerController player)
    {
        if (used) 
        {
            return;
        }

        if (GameState.Instance.currentPuzzlePhase < requiredPhase) 
        {
            return;
        }

        used = true;

        // Animación palanca
        if (leverAnimator != null)
        {
            leverAnimator.SetTrigger("On");
        }
            
        // Activar objetos
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }  

        if (triggerToActivate != null)
        {
            triggerToActivate.SetActive(true);
        }
            
        // Desactivar pared u objeto
        if (pared != null)
        {
            pared.SetActive(false);
        }         
    }
}
