using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EscenaFinal : MonoBehaviour
{
    [SerializeField] 
    private GameObject panelFinal;
    [SerializeField] 
    private PlayerController player;
    [SerializeField] 
    private Animator anim;
    [SerializeField] 
    private Animator animPlayer;
    [SerializeField] 
    private string escenaFinal;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.StopMovement(); 
            player.enabled = false;
            //anim.SetBool("IsRunning", false);
            //animPlayer.SetBool("IsWalking", false);
            animPlayer.SetTrigger("Final");
            StartCoroutine(FinalCoroutine());
        }
    }
    
    IEnumerator FinalCoroutine()
    {
        yield return new WaitForSeconds(6f);
        panelFinal.SetActive(true);

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(escenaFinal);
    }
}
