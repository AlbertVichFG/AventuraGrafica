using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera targetCamera;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.Instance.SwitchTo(targetCamera);
        }
    }
}
