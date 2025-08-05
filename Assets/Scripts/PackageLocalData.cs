using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData 
{
    private static PackageLocalData instance;
    public static PackageLocalData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PackageLocalData();
            }
            return instance;
        }
    }



    public List<PackageLocalItem> items;

    public void SavePackage()
    {
        string inventoryJson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("PackageLocalData", inventoryJson);
        PlayerPrefs.Save();
    }

    public List<PackageLocalItem> LoadPackage()
    {
        if (items != null)
        {
            return items;
        }
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            string inventoryJson = PlayerPrefs.GetString("PackageLocalData");
            PackageLocalData PackageLoadedData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);
            items = PackageLoadedData.items;
            return items;
        }
        else
        {
            items = new List<PackageLocalItem>();
            return items;
        }
    }
}


[System.Serializable]
public class PackageLocalItem
{
    public string uid;
    public int id;
    public int num;
    public int level;
    public bool isNew;

    public override string ToString()
    {
        return string.Format("uid:{0}, id:{1}, num:{2}, level:{3}, isNew:{4}", uid, id, num, level, isNew);
    }
}
