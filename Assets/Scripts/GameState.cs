using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public HashSet<string> openedDoors = new HashSet<string>();

    [SerializeField] 
    public int currentPuzzlePhase = 0;

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
