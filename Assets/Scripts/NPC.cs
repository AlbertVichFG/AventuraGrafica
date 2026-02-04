using UnityEngine;

public class NPC : MonoBehaviour
{
    [System.Serializable]
    public class DialoguePhase
    {
        [TextArea(2, 4)]
        public string[] lines;
    }

    [Header("Dialogue per Phase")]
    [SerializeField] 
    private DialoguePhase[] phases;

    [Header("Reference")]
    [SerializeField]
    private DialogueUI dialogueUI;
    



    public void Talk()
    {
        int phase = GameState.Instance.CurrentPhase;

        if (phase < 0 || phase >= phases.Length)
            return;

        dialogueUI.StartDialogue(phases[phase].lines);
    }

    void OnMouseDown()
    {
        Debug.Log("NPC CLICKED!");
        Talk();
    }
}
