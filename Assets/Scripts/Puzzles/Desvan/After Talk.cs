using UnityEngine;

public class AfterTalk : Interactable
{
    [Header("Phase requerida")]
    [SerializeField] private int requiredPhase = 1;

    [Header("Animació palanca")]
    [SerializeField] private Animator leverAnimator;


    [Header("Objectes a activar")]
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private GameObject triggerToActivate;

    private bool used = false;

    public override void Interact(PlayerController player)
    {
        if (used)
            return;

        if (GameState.Instance.currentPuzzlePhase < requiredPhase)
            return;

        used = true;

        if (leverAnimator != null)
            leverAnimator.SetTrigger("On");

        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        if (triggerToActivate != null)
            triggerToActivate.SetActive(true);
    }
}
