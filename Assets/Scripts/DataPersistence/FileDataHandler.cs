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
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "jiefence";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }
    public GameData Load(string profileId)
    {
        // using the Combine method to account for different OS path separators
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
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

                // optionally decrypt the data
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
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
    public void Save(GameData data, string profileId)
    {
        // using the Combine method to account for different OS path separators
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try
        {
            // create the directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the game data to JSON format
            string dataToStore = JsonUtility.ToJson(data, true);

            // optionally encrypt the data
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

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

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        // loop over all directories in the data directory
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            // defensive programming: check if the profile directory is empty
            // if it is, skip to the next iteration
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning($"Skipping profile {profileId} because the data file does not exist.");
                continue;
            }

            // load the game data for this profile
            GameData profileData = Load(profileId);
            // defensive programming: check if the profile data is null
            // beacause if it is then something went wrong and we should let ourselves know
            if(profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError($"Failed to load game data for profile {profileId}. The data may be corrupted or the file may not exist.");
            }
        }
        return profileDictionary;
    }

    /// <summary>
    /// JieFence：使用一个代码字通过简单的异或运算对给定的数据进行加密或解密。这是一种基本的混淆形式，不应用于安全加密。
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
