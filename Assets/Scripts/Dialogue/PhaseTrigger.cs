using UnityEngine;

public class PhaseTrigger : MonoBehaviour
{
    [SerializeField]
    private int phaseToSet;

    [SerializeField]
    private bool disableAfterTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectat: " + other.name);

        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Player detectat");

        GameState.Instance.SetPhase(phaseToSet);

        if (disableAfterTrigger)
            gameObject.SetActive(false);
    }
}