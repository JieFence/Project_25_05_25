using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : SingleTon<DataPersistenceManager>
{
    [Header("File Stroge Config")]
    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        // Load any saved game data from a file using the data handler
        gameData = dataHandler.Load();

        // if no data can be loaded, initialize a new game
        if (gameData == null)
        {
            Debug.Log("No saved game data found. Starting a new game.");
            NewGame();
        }
        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadGame(gameData);
        }
    }
    public void SaveGame()
    {
        // pass the data to other scripts so they can update the game data
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveGame(ref gameData);
        }
        // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
            
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
