using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        int door = GameManager.instance.doorToGo;

        Transform spawn = spawnPoints[door];

        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        PlayerController controller = player.GetComponent<PlayerController>();

        controller.ignoreNavMeshSnap = true;

        agent.enabled = false;

        player.transform.position = spawn.position;
        player.transform.rotation = spawn.rotation;

        agent.enabled = true;
        agent.ResetPath();

        controller.ignoreNavMeshSnap = false;
    }
}   