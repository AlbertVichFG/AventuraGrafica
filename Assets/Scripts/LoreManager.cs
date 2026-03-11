using UnityEngine;
using UnityEngine.SceneManagement;


public class LoreManager : MonoBehaviour
{
    public GameObject panelLore2;
    public GameObject panelLore3;


    public void Lore2()
    {       
        panelLore2.SetActive(true);
        
    }

    public void Lore3()
    {       
        panelLore3.SetActive(true);
        
    }

    public void StartButton()
    {       
        SceneManager.LoadScene(2);
    }


}
