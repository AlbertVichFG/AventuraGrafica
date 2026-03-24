using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    void Start()
    {
        if (GameManager.instance.comeFromLoadGame) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int door = GameManager.instance.doorToGo;

        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        PlayerController controller = player.GetComponent<PlayerController>();

        controller.ignoreNavMeshSnap = true;
        agent.enabled = false;
        player.transform.position = spawnPoints[GameManager.instance.doorToGo].position;
        player.transform.rotation = spawnPoints[GameManager.instance.doorToGo].rotation;
        agent.enabled = true;
        agent.ResetPath();
        controller.ignoreNavMeshSnap = false;
    }
}