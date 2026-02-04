using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    [SerializeField] 
    private int currentPuzzlePhase = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int CurrentPhase => currentPuzzlePhase;

    public void SetPhase(int newPhase)
    {
        currentPuzzlePhase = newPhase;
    }
}
