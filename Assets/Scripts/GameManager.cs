using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField]
    private GameData gameData;

    public int slot;
    public int doorToGo;
    public bool comeFromLoadGame;

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

    public void SaveGame()
    {
        string data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("data"+slot.ToString(), data);

        //PlayerPrefs.SetString("nombre del guardado", "Valor");
        //PlayerPrefs.SetInt("nombre del guardado", 1);
        //PlayerPrefs.SetFloat("nombre del guardado", -1.2f);
    }

    public void LoadGame()
    {
        if(PlayerPrefs.HasKey("data" + slot.ToString()) == true)
        {
            string data = PlayerPrefs.GetString("data" + slot.ToString());
            gameData = JsonUtility.FromJson<GameData>(data);
        }
    }
}
