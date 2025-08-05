using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GMcmd
{
    [MenuItem("GMcmd/读取表格")]
    public static void ReadTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        foreach (var item in packageTable.DataList)
        {
            Debug.Log($"ID: {item.id}, Type: {item.type}, Star: {item.star}, Name: {item.name}, Description: {item.description}, Skill Description: {item.skillDescription}, Image Path: {item.imagePath}, Num: {item.num}");
        }
    }
    [MenuItem("GMcmd/创建背包测试数据")]
    public static void CreateLocalPackageData()
    {
        //保存数据
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        for (int i = 1; i < 9; i++)
        {
            PackageLocalItem packageLocalItem = new()
            {
                uid = Guid.NewGuid().ToString(), // 生成唯一标识符
                id = i + 1000, // 假设ID从1000开始
                num = UnityEngine.Random.Range(1, 10), // 随机数量
                level = UnityEngine.Random.Range(1, 5), // 随机等级
                isNew = true // 默认设置为新物品
            };
            PackageLocalData.Instance.items.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SavePackage();

    }
    
    [MenuItem("GMcmd/读取背包数据")]
    public static void ReadLocalPackageData()
    {
        List<PackageLocalItem> readItems = PackageLocalData.Instance.LoadPackage();
        foreach (var item in readItems)
        {
            Debug.Log(item.ToString());
        }
    }
}
