using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disablePersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId = "test";
    [Header("File Stroge Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption = false;

    [Header("Auto Saving Configation")]
    [SerializeField] private float autoSaveTimeSeconds = 60f;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    private string selectedProfileId = "";

    private Coroutine autoSaveCoroutine;

    public static DataPersistenceManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one DataPersistenceManager in the scene.Destroying the new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (disablePersistence)
        {
            Debug.Log("Data Persistence is disabled.");
        }

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

        InitializeSelectedProfileId();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

        // start up the auto-save coroutine if persistence is enabled
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSaveCoroutine());
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        selectedProfileId = newProfileId;

        LoadGame();
    }

    public void DeleteProfileData(string profileId)
    {
        dataHandler.Delete(profileId);

        InitializeSelectedProfileId();
        LoadGame();
    }
    private void InitializeSelectedProfileId()
    {
        selectedProfileId = dataHandler.GetMostRecentlySavedProfileId();

        if (overrideSelectedProfileId)
        {
            selectedProfileId = testSelectedProfileId;
            Debug.LogWarning($"Overrode selected profile ID to: {selectedProfileId}");
        }
    }

    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        // if persistence is disabled, return right away
        if (disablePersistence)
        {
            return;
        }

        // Load any saved game data from a file using the data handler
        gameData = dataHandler.Load(selectedProfileId);

        if (gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        // if no data can be loaded, initialize a new game
        if (gameData == null)
        {
            Debug.Log("No saved game data found. A New Game needs to be started before data can be loaded.");
            return;
        }
        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadGame(gameData);
        }
    }
    public void SaveGame()
    {
        // if persistence is disabled, return right away
        if (disablePersistence)
        {
            return;
        }

        // if no game data exists, log a warning and return
        if (gameData == null)
        {
            Debug.LogWarning("No game data exists,so cannot save.You Should Start A New Game From Main Menu.");
            return;
        }

        // pass the data to other scripts so they can update the game data
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveGame(gameData);
        }

        // update the last save time
        gameData.lastSaveTime = System.DateTime.Now.ToBinary();

        // save that data to a file using the data handler
        dataHandler.Save(gameData, selectedProfileId);
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
    
    private IEnumerator AutoSaveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto-saved game data.");
        }
    }
}
