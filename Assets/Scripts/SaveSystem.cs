using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public int escenaInicio = 1;
    bool nuevaPartida = false;

    public void NuevaPartidaModo()
    {
        nuevaPartida = true;
    }

    public void CargarPartidaModo()
    {
        nuevaPartida = false;
    }

    public void SeleccionarSlot(int slot)
    {
        if (nuevaPartida)
        {
            PlayerPrefs.SetInt("slot" + slot, escenaInicio);
            PlayerPrefs.Save();

            SceneManager.LoadScene(escenaInicio);
        }
        else
        {
            if (PlayerPrefs.HasKey("slot" + slot))
            {
                int escena = PlayerPrefs.GetInt("slot" + slot);
                SceneManager.LoadScene(escena);
            }
            else
            {
                Debug.Log("Este slot está vacío");
            }
        }
    }

    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Guardados eliminados");
    }
}