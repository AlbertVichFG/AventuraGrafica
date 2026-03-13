using UnityEngine;

public class TamboretPuzzle : MonoBehaviour
{
    public static TamboretPuzzle Instance;

    public bool stoolPlaced = false;
    public bool playerOnStool = false;

    void Awake()
    {
        Instance = this;
    }
}
