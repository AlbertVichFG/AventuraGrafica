using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    
    [SerializeField] 
    private GameObject panelPause;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if(panelPause.activeInHierarchy == false)
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
        SceneManager.LoadScene(0);
    }
    public void saveGame()
    {
        GameManager.instance.GetGameData.SceneSave = SceneManager.GetActiveScene().buildIndex;
        GameManager.instance.SaveGame();
    }
}
