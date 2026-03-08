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

    [Header("References")]
    [SerializeField]
    private DialogueUI dialogueUI;

    [SerializeField]
    private PlayerController player; 

    private int[] phaseIndexes;

    void Awake()
    {
        phaseIndexes = new int[phases.Length];

        player = FindFirstObjectByType<PlayerController>();
    }

    public void Talk()
    {
        int phase = GameState.Instance.CurrentPhase;

        if (phase < 0 || phase >= phases.Length)
            return;

        string[] lines = phases[phase].lines;

        if (lines.Length == 0)
            return;

        int index = phaseIndexes[phase];

        // bloquejar moviment
        player.StopMovement();

        dialogueUI.SetPlayer(player);

        // mostrar frase
        dialogueUI.ShowLine(lines[index]);

        phaseIndexes[phase]++;

        if (phaseIndexes[phase] >= lines.Length)
            phaseIndexes[phase] = lines.Length - 1;
    }
}