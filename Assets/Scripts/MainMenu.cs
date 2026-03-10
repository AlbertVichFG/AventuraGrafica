using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject panelAjustes;
    public string escenaJuego;
    public GameObject panelSlots;
    private bool newGame;
    

    public void StartButton(bool _newGame)
    {       
        panelSlots.SetActive(true);
        newGame = _newGame;
    }
    public void SlotButton(int _slot)
    {
        if (newGame == true)
        {
            SceneManager.LoadScene(1);       
        }
        else
        {
            if (PlayerPrefs.HasKey("data" + _slot.ToString()))
            {
                GameManager.instance.LoadGame();
                GameManager.instance.comeFromLoadGame = true;
                SceneManager.LoadScene(GameManager.instance.GetGameData.SaveScene);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
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
