using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField]
    private GameData gameData;

    public int slot;
    public int doorToGo;
    public bool comeFromLoadGame;
    public int currentSlot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Para test
        if(Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public GameData GetGameData
    {
        get { return gameData; }
        set { gameData = value;}
    }

    public void SaveGame(int slot)
    {
        string json = JsonUtility.ToJson(GetGameData);
        PlayerPrefs.SetString("data" + slot.ToString(), json);
        PlayerPrefs.Save();
    }

    public void LoadGame(int slot)
    {
        string key = "data" + slot.ToString();
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            GetGameData = JsonUtility.FromJson<GameData>(json);
        }
    }


    
}
