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

    // Guarda quin índex toca per cada fase
    private int[] phaseIndexes;

    void Awake()
    {
        phaseIndexes = new int[phases.Length]; 
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

        // Mostrar frase actual
        dialogueUI.ShowLine(lines[index]); 

        // Avançar per la seguent vegada
        phaseIndexes[phase]++; 

        // Imprimir ultima frase quan s'acaba
        if (phaseIndexes[phase] >= lines.Length) 
            phaseIndexes[phase] = lines.Length - 1; 
    }
}
