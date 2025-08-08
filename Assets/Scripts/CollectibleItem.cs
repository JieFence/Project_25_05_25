using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour,IDataPersistence
{
    [SerializeField] private string id;

    [SerializeField] private bool isCollected = false;


    [ContextMenu("Generate a guid ID")]
    public void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        // if (string.IsNullOrEmpty(id))
        // {
        //     GenerateGuid();
        // }
    }
    public void LoadGame(GameData gameData)
    {
        gameData.collectedItems.TryGetValue(id, out isCollected);
        if (isCollected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveGame(GameData gameData)
    {
        if (gameData.collectedItems.ContainsKey(id))
        {
            gameData.collectedItems[id] = isCollected;
        }
        else
        {
            gameData.collectedItems.Add(id, isCollected);
        }
    }
}
 