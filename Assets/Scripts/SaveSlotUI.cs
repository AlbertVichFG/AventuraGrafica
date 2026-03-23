using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlotUI : MonoBehaviour
{
    public int slotIndex;

    public GameObject emptyPanel;
    public GameObject dataPanel;

    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI dateTxt;
    public TextMeshProUGUI sceneTxt;

    public Button loadButton;
    public Button deleteButton;

    private MainMenu mainMenu;

    void Start()
    {
        mainMenu = FindFirstObjectByType<MainMenu>();
        Refresh();
    }

    public void Refresh()
    {
        GameData data = SaveSystem.Load(slotIndex);
        bool isEmpty = data == null;

        emptyPanel.SetActive(isEmpty);
        dataPanel.SetActive(!isEmpty);
        loadButton.interactable = !isEmpty;
        deleteButton.interactable = !isEmpty;

        if (!isEmpty)
        {
            timeTxt.text = GameManager.FormatPlayTime(data.totalPlayTime);
            dateTxt.text = data.lastSaveDate;
            sceneTxt.text = string.IsNullOrEmpty(data.sceneName) ? "Escena " + data.SaveScene : data.sceneName;
        }
    }

    public void OnSlotButton() => mainMenu.SlotButton(slotIndex);
    public void OnDeleteButton() => mainMenu.DeleteSlotButton(slotIndex);
}