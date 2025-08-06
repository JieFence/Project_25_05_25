using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
// This class is responsible for handling file operations related to game data persistence.
// It will include methods for saving and loading game data to and from files.
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public GameData Load()
    {
        // using the Combine method to account for different OS path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // deserialize the JSON data to a GameData object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load game data: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        // using the Combine method to account for different OS path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // create the directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the game data to JSON format
            string dataToStore = JsonUtility.ToJson(data, true);

            // write the JSON data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save game data: " + fullPath + "\n" + e);
        }
    }
}
