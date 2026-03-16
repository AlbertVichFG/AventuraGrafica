using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera targetCamera;
    private bool isActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            CameraManager.Instance.SwitchTo(targetCamera);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            // Si la cámara activa ya no es la nuestra, reclamarla
            if (!targetCamera.gameObject.activeSelf)
            {
                CameraManager.Instance.SwitchTo(targetCamera);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isActive = false;
    }
}
