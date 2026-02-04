using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject panel;
    [SerializeField] 
    private TMP_Text dialogueText;

    public bool IsOpen { get; private set; }

    void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(string text)
    {
        IsOpen = true;
        dialogueText.text = text;
        panel.SetActive(true);
    }

    public void Hide()
    {
        IsOpen = false;
        panel.SetActive(false);
    }
}
