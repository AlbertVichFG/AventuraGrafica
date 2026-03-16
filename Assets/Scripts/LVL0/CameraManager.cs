using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public Camera[] cameras;
    private VirtualCursorController cursor;
    private PlayerController player;

    void Awake()
    {
        Instance = this;
        cursor = FindFirstObjectByType<VirtualCursorController>();
        player = FindFirstObjectByType<PlayerController>();
        SwitchTo(cameras[0]);
    }

    public void SwitchTo(Camera target)
    {
        // Validar que la cámara pertenece al array gestionado
        if (target == null || System.Array.IndexOf(cameras, target) < 0)
        {
            Debug.LogWarning("CameraManager: cámara inválida o no registrada.");
            return;
        }

        foreach (Camera cam in cameras)
            cam.gameObject.SetActive(cam == target); // Solo enciende la target

        // Actualitzar camera usada pel raycast
        cursor.SetActiveCamera(target);
        player.SetActiveCamera(target);
    }
}
