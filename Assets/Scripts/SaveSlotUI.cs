using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public int slotIndex;
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI timeTxt;



    private MainMenu mainMenu;

    void Start()
    {
        mainMenu = FindFirstObjectByType<MainMenu>();
        Refresh();
    }

    public void Refresh()
    {
        bool isEmpty = !SaveSystem.SlotExists(slotIndex);

        if (!isEmpty && levelTxt != null)
    {
        GameData data = SaveSystem.Load(slotIndex);
        levelTxt.text = data.sceneName;
        timeTxt.text = GameManager.FormatPlayTime(data.totalPlayTime);
        }

        if(isEmpty == true)
        {
            levelTxt.text = "";
            timeTxt.text = "";

        } 
    }

    public void OnSlotButton() => mainMenu.SlotButton(slotIndex);
    public void OnDeleteButton() => mainMenu.DeleteSlotButton(slotIndex);
}
