using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public int slotIndex;


    private MainMenu mainMenu;

    void Start()
    {
        mainMenu = FindFirstObjectByType<MainMenu>();
        Refresh();
    }

    public void Refresh()
    {
        bool isEmpty = !SaveSystem.SlotExists(slotIndex);
    }

    public void OnSlotButton() => mainMenu.SlotButton(slotIndex);
    public void OnDeleteButton() => mainMenu.DeleteSlotButton(slotIndex);
}
