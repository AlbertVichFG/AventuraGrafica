using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private int door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.doorToGo = door;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}