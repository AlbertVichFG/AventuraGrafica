using UnityEngine;

public class DisableNPCTriggers : MonoBehaviour
{

    [SerializeField]
    private NPC[] npcsToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        foreach (NPC npc in npcsToDisable)
        {
            if (npc != null)
            {
                npc.enabled = false;
            }
        }
    }
}
