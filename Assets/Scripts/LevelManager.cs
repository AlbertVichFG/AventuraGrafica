using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] 
    private Transform[] spawnPoints;

    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (GameManager.instance.comeFromLoadGame)
        {
            GameManager.instance.comeFromLoadGame = false;
        }
        else
        {
            int door = GameManager.instance.doorToGo;

            player.position = spawnPoints[door].position;
            player.rotation = spawnPoints[door].rotation;
        }
    }
}
