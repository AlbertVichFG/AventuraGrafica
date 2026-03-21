using UnityEngine;

public class DoorBiblio : MonoBehaviour
{
    [SerializeField]
    private Animator doorAnimator;
    [SerializeField]
    private AudioClip sfxDoor;

    public void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("PuertaAbierta");
            AudioManager.instance.PlaySFX(sfxDoor, transform.position);
        }      
    }
}
