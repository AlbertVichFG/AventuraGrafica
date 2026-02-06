using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Typewriter Settings")]
    [SerializeField] private float letterSpeed = 0.03f;

    public bool IsOpen { get; private set; }

    private Coroutine typingCoroutine;
    private bool isTyping;
    private string fullLine;

    // Evita que el primer click tanqui el panel instant
    [SerializeField]
    private bool canClose = false;

    

    void Awake()
    {
        panel.SetActive(false);

    }

    private void Start()
    {
        
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
            //Si encara està escrivint skip
            if (isTyping)
            {
                FinishTypingInstant();
                return;
            }

            //Si ja ha acabat, permet tancar només després d’un frame
            if (canClose)
            {
                Hide();
            }
        }
    }

    // Mostrar una frase amb efecte
    public void ShowLine(string line)
    {
        IsOpen = true;
        panel.SetActive(true);

        // Reset estat
        canClose = false;

        // Cancel·lar typing anterior
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        fullLine = line;
        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    // Coroutine lletra per lletra
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

        // Esperar 1 frame abans de permetre tancar
        yield return null;
        canClose = true;
    }

    // Click mentre escriu mostrar tot instant
    private void FinishTypingInstant()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = fullLine;
        isTyping = false;

        // Permetre tancar en el següent click
        canClose = true;
    }

    // Tancar panel
    public void Hide()
    {
        IsOpen = false;
        panel.SetActive(false);
    }
}
