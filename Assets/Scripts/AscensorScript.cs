using UnityEngine;
using UnityEngine.SceneManagement;

public class AscensorScript : Interactable
{
    [SerializeField] private string sceneToLoad;
    public bool isOn = false;

    public override void Interact(PlayerController player)
    {
        if (isOn == false) 
        {
            return;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
