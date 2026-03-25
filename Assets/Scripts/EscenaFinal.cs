using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;

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
    [SerializeField]
    private GameObject CanvasMouse; 

    [SerializeField]
    private AudioClip sfxDoor;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            player.enabled = false;
            player.GetComponent<NavMeshAgent>().Stop();
            animPlayer.SetBool("IsRuning", false);
            animPlayer.SetBool("IsWalking", false);
            
            AudioManager.instance.PlaySFX(sfxDoor, transform.position);
            anim.SetTrigger("Final");
            StartCoroutine(FinalCoroutine());
        }
    }
    
    IEnumerator FinalCoroutine()
    {
        yield return new WaitForSeconds(6f);
        panelFinal.SetActive(true);
        CanvasMouse.SetActive(false);

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(escenaFinal);
    }
}
