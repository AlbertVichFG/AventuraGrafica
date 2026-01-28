using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public Camera[] cameras;

    void Awake()
    {
        Instance = this;
        SwitchTo(cameras[0]);
    }

    public void SwitchTo(Camera target)
    {
        foreach (Camera cam in cameras)
            cam.gameObject.SetActive(false);

        target.gameObject.SetActive(true);
    }
}
