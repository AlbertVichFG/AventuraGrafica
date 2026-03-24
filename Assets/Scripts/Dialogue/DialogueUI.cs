using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] 
    private GameObject panel;
    [SerializeField] 
    private TMP_Text dialogueText;

    [Header("Typewriter Settings")]
    [SerializeField] 
    private float letterSpeed = 0.03f;


    string[] dialogueLines;
    int currentLine;

    [SerializeField] 
    private float movementUnlockDelay = 0.2f;

    public bool IsOpen { get; private set; }

    private Coroutine typingCoroutine;
    private bool isTyping;
    private string fullLine;

    [SerializeField]
    private bool canClose = false;

    private PlayerController player; // NOVA REFERÈNCIA

    void Awake()
    {
        panel.SetActive(false);
    }

    public void SetPlayer(PlayerController p)
    {
        player = p;
    }

    void Update()
    {
        bool press =
            Input.GetMouseButtonDown(0) ||
            (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame);

        if (!IsOpen)
            return;

        if (press)
        {
            if (isTyping)
            {
                FinishTypingInstant();
                return;
            }

            if (canClose)
            {
                currentLine++;

                if (dialogueLines != null && currentLine < dialogueLines.Length)
                {
                    ShowLine(dialogueLines[currentLine]);
                }
                else
                {
                    Hide();
                }
            }
        }
    }

    public void ShowLine(string line)
    {
        IsOpen = true;
        panel.SetActive(true);

        canClose = false;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        fullLine = line;
        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }

        isTyping = false;

        yield return null;
        canClose = true;
    }
    public void ShowDialogue(string[] lines)
    {
        dialogueLines = lines;
        currentLine = 0;

        ShowLine(dialogueLines[currentLine]);
    } 

    private void FinishTypingInstant()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = fullLine;
        isTyping = false;
        canClose = true;
    }

    public void Hide()
    {
        IsOpen = false;
        panel.SetActive(false);

        if (player != null)
            StartCoroutine(UnlockMovementDelayed());
    }

    IEnumerator UnlockMovementDelayed()
    {
        yield return new WaitForSeconds(movementUnlockDelay);

        if (player != null)
        {
            player.UnlockMovement();
            player = null;
        }
    }
}