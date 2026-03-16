using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject panelAjustes;
    public string escenaJuego;
    public GameObject panelSlots;
    private bool newGame;
    private int selectedSlot; // este será el slot en el que estamos jugando
    

    public void StartButton(bool _newGame)
    {       
        panelSlots.SetActive(true);
        newGame = _newGame;
    }

   
    public void SlotButton(int _slot)
    {
        // Guarda el slot seleccionado en GameManager para usarlo luego al guardar
        GameManager.instance.currentSlot = _slot;

        if (newGame)
        {
            // Nueva partida: limpia datos previos del slot y empieza la escena de juego
            PlayerPrefs.DeleteKey("data" + _slot.ToString());
            SceneManager.LoadScene(1);   
            GameManager.instance.comeFromLoadGame = false;
    
        }
        else
        {
            // Cargar partida
            string key = "data" + _slot.ToString();
            if (PlayerPrefs.HasKey(key))
            {
                GameManager.instance.LoadGame(_slot);   // Carga datos del slot seleccionado
                GameManager.instance.comeFromLoadGame = true;
                SceneManager.LoadScene(GameManager.instance.GetGameData.SceneSave);
            }
            else
            {
                Debug.LogWarning("No hay partida guardada en el slot " + _slot);
                // Opcional: puedes iniciar nueva partida si quieres
                // SceneManager.LoadScene(escenaJuego);
            }
        }
        
        // Cierra el panel de selección de slots
        panelSlots.SetActive(false);
    }


    public void AbrirAjustes()
    {
        panelAjustes.SetActive(true);
    }

    public void CerrarAjustes()
    {
        panelAjustes.SetActive(false);
    }

    public void SalirJuego()
    {
        Debug.Log("Salir del juego");

        Application.Quit();
    }
    public void borrarPartidas()
    {
        PlayerPrefs.DeleteAll();
    }
    public void cerrarSlots()
    {
        panelSlots.SetActive(false);
    }
}
