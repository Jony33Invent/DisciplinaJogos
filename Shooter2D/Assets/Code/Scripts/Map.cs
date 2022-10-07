using System;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

[Serializable]
public struct Map
{
    public string MapName;

    [Scene]
    public int ActiveScene;

    [Scene]
    public int[] LoadedScenes;




    public static Map? SearchMap(Map[] mapList, string mapName)
    {
        foreach (Map map in mapList)
        {
            if (string.Compare(map.MapName, mapName) == 0)
            {
                return map;
            }
        }
        return null;
    }
}