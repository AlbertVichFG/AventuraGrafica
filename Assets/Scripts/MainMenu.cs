using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject panelAjustes;
    public string escenaJuego;

    public void NuevaPartida()
    {
        SceneManager.LoadScene(1);
    }

    public void CargarPartida()
    {
        Debug.Log("Cargar partida...");
        // Aquí luego puedes cargar datos guardados
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
