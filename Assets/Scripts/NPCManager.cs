using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("Dialogue - Sequential")]
    [TextArea(2, 4)]
    [SerializeField] 
    private string[] sequentialLines;

    [Header("Dialogue - Random")]
    [TextArea(2, 4)]
    [SerializeField] 
    private string[] randomLines;

    [SerializeField] 
    private DialogueUI dialogueUI;

    private int index = 0;

    public void Talk()
    {
        string line = GetLine();
        dialogueUI.Show(line);
    }

    private string GetLine()
    {
        // Frases en ordre
        if (index < sequentialLines.Length)
            return sequentialLines[index++];

        // Frases random
        if (randomLines.Length > 0)
            return randomLines[Random.Range(0, randomLines.Length)];

        return "...";
    }
}
