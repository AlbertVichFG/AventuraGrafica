using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject panelAjustes;
    public string escenaJuego;
    public GameObject panelSlots;
    public SaveSystem saveSystem;

    public void NuevaPartida()
    {
        panelSlots.SetActive(true);
        saveSystem.NuevaPartidaModo();
    }


    public void CargarPartida()
    {
        panelSlots.SetActive(true);
        saveSystem.CargarPartidaModo();
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
}
