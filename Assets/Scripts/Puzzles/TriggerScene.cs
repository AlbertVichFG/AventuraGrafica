using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private int door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
            GameManager.instance.doorToGo = door;
        }
    }
}
