using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameData gameData;

    public GameData GetGameData
    {
        get { return gameData; }
        set { gameData = value; }
    }

    public int doorToGo;
    public bool comeFromLoadGame;
    public int currentSlot = -1;

    // Indica que hay que autoguardar en cuanto cargue la siguiente escena
    private bool pendingAutoSave = false;

    private float sessionTime = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (gameData == null)
            {
                gameData = new GameData();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void Update()
    {
        if (gameData != null && currentSlot >= 0)
            sessionTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.B))
        {
            SaveSystem.Delete(0);
            SaveSystem.Delete(1);
            SaveSystem.Delete(2);
            Debug.Log("[GameManager] Todos los slots borrados.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu") return;

        // SceneInfo ya existe aquí, guardamos el nombre bonito
        if (gameData != null)
        {
            SceneInfo sceneInfo = FindFirstObjectByType<SceneInfo>();
            Debug.Log("SceneInfo encontrado: " + (sceneInfo != null ? sceneInfo.nombreNivel : "NULL"));

            if (sceneInfo != null)
                gameData.sceneName = sceneInfo.nombreNivel;
        }

        // Si venimos de cruzar una puerta, guardamos ahora que
        // la escena destino ya está cargada y sceneName es correcto
        if (pendingAutoSave)
        {
            pendingAutoSave = false;
            StartCoroutine(AutoSaveNextFrame());
        }

        if (comeFromLoadGame && gameData != null)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null)
                player.LoadFromSaveData(gameData);

            if (InventoryManager.instance != null)
                StartCoroutine(LoadInventoryNextFrame());

            comeFromLoadGame = false;
        }
    }

    // Esperamos un frame para que el jugador e inventario estén inicializados
    private IEnumerator AutoSaveNextFrame()
    {
        yield return null;
        SaveGame();
        Debug.Log("[GameManager] Autoguardado completado en nueva escena.");
    }

    private IEnumerator LoadInventoryNextFrame()
    {
        yield return null;
        InventoryManager.instance.LoadFromSaveData(gameData);
    }

    public void NewGame(int slot)
    {
        currentSlot = slot;
        sessionTime = 0f;
        gameData = new GameData();
        comeFromLoadGame = false;
        SceneManager.LoadScene(gameData.SaveScene);
    }

    public void SaveGame()
    {
        if (currentSlot < 0 || gameData == null)
        {
            Debug.LogWarning("[GameManager] No hay slot activo.");
            return;
        }

        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
            player.PopulateSaveData(gameData);

        if (InventoryManager.instance != null)
            InventoryManager.instance.PopulateSaveData(gameData);

        gameData.SaveScene = SceneManager.GetActiveScene().buildIndex;
        gameData.totalPlayTime += sessionTime;
        gameData.lastSaveDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        sessionTime = 0f;

        SaveSystem.Save(gameData, currentSlot);
        Debug.Log("[GameManager] Partida guardada correctamente.");
    }

    // Llamado desde TriggerScene: marca que hay que autoguardar
    // en cuanto cargue la escena destino
    public void RequestAutoSave()
    {
        pendingAutoSave = true;
    }

    public void LoadGame(int slot)
    {
        GameData data = SaveSystem.Load(slot);
        if (data == null)
        {
            Debug.LogWarning("[GameManager] Ranura vacía.");
            return;
        }

        currentSlot = slot;
        sessionTime = 0f;
        gameData = data;
        comeFromLoadGame = true;

        SceneManager.LoadScene(gameData.SaveScene);
    }

    public void DeleteSlot(int slot) => SaveSystem.Delete(slot);

    public void RegisterPickedUpItem(string key)
    {
        if (gameData != null && !gameData.pickedUpItems.Contains(key))
        {
            gameData.pickedUpItems.Add(key);
        }
    }

    public bool IsItemPickedUp(string key)
    {
        return gameData != null && gameData.pickedUpItems.Contains(key);
    }

    public static string FormatPlayTime(float seconds)
    {
        int h = (int)(seconds / 3600);
        int m = (int)(seconds % 3600 / 60);
        int s = (int)(seconds % 60);
        return h > 0 ? $"{h}h {m:D2}m {s:D2}s" : $"{m}m {s:D2}s";
    }
}