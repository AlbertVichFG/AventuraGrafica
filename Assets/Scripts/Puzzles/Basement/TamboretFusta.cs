using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TamboretFusta : Interactable
{
    [SerializeField]
    private Transform jumpX,jumpY;

    public override void Interact(PlayerController player)
    {
        Debug.Log("Player puja al tamboret");
        player.StartCoroutine(JumpUp(player));
    }

    IEnumerator JumpUp(PlayerController player)
    {
        player.StopMovement();

        player.ignoreNavMeshSnap = true;
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        agent.enabled = false;

        Animator anim = player.GetComponentInChildren<Animator>();
        anim.SetTrigger("Jump");

        float t = 0;
        Vector3 start = jumpX.position;
        Vector3 end = jumpY.position;

        while (t < 1)
        {
            t += Time.deltaTime * 2f;
            player.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        // POSICI� FINAL EXACTA
        player.transform.position = jumpY.position;
        TamboretPuzzle.playerOnStool = true;
        player.UnlockMovement();
    }
}
