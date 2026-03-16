using UnityEngine;

public class DoorBiblio : MonoBehaviour
{
    [SerializeField]
    private Animator doorAnimator;

    public void OpenDoor()
    {
        if (doorAnimator != null)
            doorAnimator.SetTrigger("PuertaAbierta");
    }
}
