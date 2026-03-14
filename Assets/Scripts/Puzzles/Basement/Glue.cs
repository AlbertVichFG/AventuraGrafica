using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Glue : Interactable
{
    [SerializeField] private ItemData glueItem;
    [SerializeField] private DialogueUI dialogueUI;

    [Header("Jump Points")]
    [SerializeField] private Transform jumpY;
    [SerializeField] private Transform jumpX;

    [TextArea]
    [SerializeField] private string needSomethingLine;

    public override void Interact(PlayerController player)
    {
        dialogueUI.SetPlayer(player);

        if (!TamboretPuzzle.playerOnStool)
        {
            dialogueUI.ShowLine(needSomethingLine);
            return;
        }

        player.StartCoroutine(PickupSequence(player));
    }

    IEnumerator PickupSequence(PlayerController player)
    {
        player.StopMovement();

        Animator anim = player.GetComponentInChildren<Animator>();
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

        agent.updateRotation = false;

        // girar cap a la pega
        Vector3 dir = jumpY.position - player.transform.position;
        dir.y = 0;
        player.transform.rotation = Quaternion.LookRotation(dir);

        anim.SetTrigger("PickItem");

        // esperar que entri a l'animació
        yield return null;

        float length = anim.GetCurrentAnimatorStateInfo(0).length;

        // esperar que acabi
        yield return new WaitForSeconds(length);

        InventoryManager.instance.AddItem(glueItem);

        yield return JumpDown(player);

        agent.updateRotation = true;


        Destroy(gameObject);
    }
   
    IEnumerator JumpDown(PlayerController player)
    {
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        Animator anim = player.GetComponentInChildren<Animator>();

        anim.SetTrigger("Jump");

        float t = 0;
        Vector3 start = jumpY.position;
        Vector3 end = jumpX.position;

        while (t < 1)
        {
            t += Time.deltaTime * 2f;
            player.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        player.transform.position = jumpX.position;

        agent.enabled = true;

        TamboretPuzzle.playerOnStool = false;

        player.ignoreNavMeshSnap = false;

        player.UnlockMovement();
    }
}