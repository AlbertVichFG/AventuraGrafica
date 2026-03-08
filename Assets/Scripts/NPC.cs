using UnityEngine;

public class NPC : Interactable
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


    private PlayerController player; 
    private int[] phaseIndexes;

    void Awake()
    {
        phaseIndexes = new int[phases.Length];

        player = FindFirstObjectByType<PlayerController>();
    }

    public override void Interact(PlayerController p)
    {
        player = p;

        int phase = GameState.Instance.CurrentPhase;

        if (phase < 0 || phase >= phases.Length)
            return;

        string[] lines = phases[phase].lines;

        if (lines.Length == 0)
            return;

        int index = phaseIndexes[phase];

        // bloquejar moviment
        player.StopMovement();

        // configurar dialogue UI
        dialogueUI.SetPlayer(player);

        // mostrar frase
        dialogueUI.ShowLine(lines[index]);

        // avançar index
        phaseIndexes[phase]++;

        if (phaseIndexes[phase] >= lines.Length)
            phaseIndexes[phase] = lines.Length - 1;
    }
}