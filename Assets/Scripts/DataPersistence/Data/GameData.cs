using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> collectedItems;

    public GameData()
    {
        deathCount = 0;
        playerPosition = Vector3.zero;
        collectedItems = new SerializableDictionary<string, bool>();

    }


}
