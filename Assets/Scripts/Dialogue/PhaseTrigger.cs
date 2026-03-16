using UnityEngine;

public class PhaseTrigger : MonoBehaviour
{
    [SerializeField] 
    private int phaseToSet;
    [SerializeField] 
    private bool disableAfterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameState.Instance.currentPuzzlePhase = phaseToSet;

        if (disableAfterTrigger)
            gameObject.SetActive(false);
    }
}
