using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject panelAjustes;
    public GameObject panelSlots;
    public GameObject panelConfirm;
    public TextMeshProUGUI confirmText;
    public SaveSlotUI[] slots;

    private bool newGame;
    private System.Action pendingAction;

    public void StartButton(bool _newGame)
    {
        newGame = _newGame;
        panelSlots.SetActive(true);
        RefreshSlots();
    }

    public void SlotButton(int _slot)
    {
        GameManager.instance.currentSlot = _slot;

        if (newGame)
        {
            if (SaveSystem.SlotExists(_slot))
            {
                RequestConfirm(
                    $"¿Sobreescribir la Ranura {_slot + 1}?\nSe perderán todos los datos.",
                    () =>
                    {
                        SaveSystem.Delete(_slot);
                        GameManager.instance.NewGame(_slot);
                        panelSlots.SetActive(false);
                    }
                );
            }
            else
            {
                GameManager.instance.NewGame(_slot);
                panelSlots.SetActive(false);
            }
        }
        else
        {
            if (SaveSystem.SlotExists(_slot))
            {
                GameManager.instance.LoadGame(_slot);
                panelSlots.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No hay partida guardada en el slot " + _slot);
            }
        }
    }

    public void DeleteSlotButton(int _slot)
    {
        RequestConfirm(
            $"¿Borrar la Ranura {_slot + 1}?\nEsta acción no se puede deshacer.",
            () =>
            {
                SaveSystem.Delete(_slot);
                RefreshSlots();
            }
        );
    }

    private void RefreshSlots()
    {
        foreach (var s in slots) s.Refresh();
    }

    private void RequestConfirm(string message, System.Action onConfirm)
    {
        confirmText.text = message;
        pendingAction = onConfirm;
        panelConfirm.SetActive(true);
    }

    public void OnConfirmYes()
    {
        pendingAction?.Invoke();
        panelConfirm.SetActive(false);
    }

    public void OnConfirmNo() => panelConfirm.SetActive(false);

    public void AbrirAjustes() => panelAjustes.SetActive(true);
    public void CerrarAjustes() => panelAjustes.SetActive(false);
    public void cerrarSlots() => panelSlots.SetActive(false);

    public void borrarPartidas()
    {
        SaveSystem.Delete(0);
        SaveSystem.Delete(1);
        SaveSystem.Delete(2);
        RefreshSlots();
    }

    public void SalirJuego()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
