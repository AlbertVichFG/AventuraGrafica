using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject panel;
    [SerializeField] 
    private TMP_Text dialogueText;
    [SerializeField] 
    private PlayerController player;

    private string[] currentLines;
    private int index;

    public bool IsOpen { get; private set; }

    void Awake()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (!IsOpen)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialogue(string[] lines)
    {
        if (lines == null || lines.Length == 0)
            return;

        currentLines = lines;
        index = 0;

        IsOpen = true;
        panel.SetActive(true);

        dialogueText.text = currentLines[index];

        // Aturem el temps de moviment del player
        player.StopMovement();
    }

    private void NextLine()
    {
        index++;

        if (index >= currentLines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = currentLines[index];
    }

    private void EndDialogue()
    {
        IsOpen = false;
        panel.SetActive(false);
        player.ResumeMovement();
    }
}
