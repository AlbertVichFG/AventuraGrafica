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
        foreach (Camera cam in cameras)
            cam.gameObject.SetActive(false);

        target.gameObject.SetActive(true);

        //actualitzar camera usada pel raycast
        cursor.SetActiveCamera(target);
        player.SetActiveCamera(target);
    }
}
