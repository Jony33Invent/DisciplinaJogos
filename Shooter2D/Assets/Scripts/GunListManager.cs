using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "GunListManager", menuName = "Shooter2D/GunListManager", order = 0)]
public class GunListManager : ScriptableObject
{
    [SerializeField] private GunEntry[] GunList;

    public GameObject GetRandomWeapon()
    {
        int i;
        // Gerar números até gerar uma arma válida
        do
        {
            i = UnityEngine.Random.Range(0, GunList.Length);
        } while (!GunList[i].isAvailable);

        return GunList[i].gunPrefab;
    }

    public void UnlockGun(int index)
    {
        GunList[index].isAvailable = true;
    }

    public void Save()
    {
        string saveDirectoryPath = Application.persistentDataPath + "/saveData";
        string saveFilePath = saveDirectoryPath + "weapons.cow";
        BinaryFormatter bf = new BinaryFormatter();

        if (!Directory.Exists(saveDirectoryPath))
        {
            Directory.CreateDirectory(saveDirectoryPath);
        }
        FileStream saveFile = File.Create(saveFilePath);

        string json = JsonUtility.ToJson(this);
        bf.Serialize(saveFile, json);

        saveFile.Close();
    }
    public void Load()
    {
        string saveDirectoryPath = Application.persistentDataPath + "/saveData";
        string saveFilePath = saveDirectoryPath + "weapons.cow";
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(saveFilePath))
        {
            FileStream saveFile = File.Open(saveFilePath, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(saveFile), this);
            saveFile.Close();
        }
    }
}

[Serializable]
public struct GunEntry
{
    public GameObject gunPrefab;
    public bool isAvailable;
}