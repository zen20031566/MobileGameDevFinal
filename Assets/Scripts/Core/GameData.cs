using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public List<SceneGroup> LevelsList;
    public List<CosmeticData> CosmeticsList;

    public Dictionary<string, SceneGroup> LevelDict;
    public Dictionary<string, CosmeticData> CosmeticDict;

    public void OnEnable()
    {
        LevelsList = new List<SceneGroup>();
        CosmeticsList = new List<CosmeticData>();
        LevelDict = new Dictionary<string, SceneGroup>();
        CosmeticDict = new Dictionary<string, CosmeticData>();

        SceneGroup[] levelData = Resources.LoadAll<SceneGroup>("Datas/SceneGroups/Levels");
        CosmeticData[] cosmeticData = Resources.LoadAll<CosmeticData>("Datas/Cosmetics");

        foreach(SceneGroup level in levelData)
        {
            LevelsList.Add(level);
            LevelDict[level.Name] = level;
        }

        foreach(CosmeticData cosmetic in cosmeticData)
        {
            if (cosmetic.Id == "None")
                CosmeticsList.Insert(0, cosmetic);
            else
                CosmeticsList.Add(cosmetic);


            CosmeticDict[cosmetic.Id] = cosmetic;
        }
    }

    public SceneGroup GetLevel(string id)
    {
        if (LevelDict.TryGetValue(id, out SceneGroup level))
        {
            return level;   
        }

        Debug.LogError(id + "Level doesnt exist");
        return null;
    }

    public CosmeticData GetCosmeticData(string id)
    {
        if (CosmeticDict.TryGetValue(id, out CosmeticData data))
        {
            return data;
        }

        Debug.LogError(id + "Cosmetic doesnt exist");
        return null;
    }

}
