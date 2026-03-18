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
        Debug.LogError(door + " " + GameManager.instance.doorToGo);
        Debug.LogError(spawnPoints[door].name);
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        PlayerController controller = player.GetComponent<PlayerController>();

        controller.ignoreNavMeshSnap = true;

        agent.enabled = false;
        Debug.LogError(spawn.position);
        player.transform.position = spawnPoints[GameManager.instance.doorToGo].position;//spawn.position;
        player.transform.rotation = spawnPoints[GameManager.instance.doorToGo].rotation;//spawn.rotation;

        agent.enabled = true;
        agent.ResetPath();

        controller.ignoreNavMeshSnap = false;
    }
}   