using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField] 
    private GameObject panelPause;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    void Start()
    {

    }

    void Update()
    {
        bool keyboardPause = Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;
        bool gamepadPause = Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame;

        if (keyboardPause || gamepadPause)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (panelPause.activeInHierarchy == false)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            panelPause.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ContinuarButton()
    {  
        panelPause.SetActive(false);
        Time.timeScale = 1;

        Cursor.visible = false;
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void saveGame()
    {
        int slot = GameManager.instance.currentSlot;
        GameManager.instance.GetGameData.SceneSave = SceneManager.GetActiveScene().buildIndex;
        GameManager.instance.SaveGame(slot);
        Debug.Log("Guardado automáticamente en slot " + slot);

        panelPause.SetActive(false); // cierra panel de pausa
        Time.timeScale = 1;
    }
}
