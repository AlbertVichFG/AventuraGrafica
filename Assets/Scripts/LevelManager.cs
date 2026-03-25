using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    void Awake()
    {
        if (GameManager.instance.comeFromLoadGame) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int door = GameManager.instance.doorToGo;
        Debug.Log(door); 
        PlayerController controller = player.GetComponent<PlayerController>();

        
        player.transform.position = spawnPoints[door].position;
        player.transform.rotation = spawnPoints[door].rotation;
        Debug.Log(spawnPoints[door].position);
        Debug.Log(player.transform.position);
        
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.transform.position);
        if (player.transform.position != spawnPoints[GameManager.instance.doorToGo].position)
        {

            player.transform.position = spawnPoints[GameManager.instance.doorToGo].position;
            player.transform.rotation = spawnPoints[GameManager.instance.doorToGo].rotation;
        }
        else
        {
            Invoke("Start", 0.02f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Awake();
        }
    }
}