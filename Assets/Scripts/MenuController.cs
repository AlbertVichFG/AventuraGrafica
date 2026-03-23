using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelPause;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 6"))
            Pause();
    }

    public void Pause()
    {
        if (!panelPause.activeInHierarchy)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ContinuarButton()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        GameManager.instance.SaveGame();
        panelPause.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Guardado en slot " + GameManager.instance.currentSlot);
    }
}
