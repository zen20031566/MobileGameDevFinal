using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public List<SceneGroup> LevelsList = new List<SceneGroup>();
    public List<CosmeticData> CosmeticsList = new List<CosmeticData>();

    public Dictionary<string, SceneGroup> LevelDict = new Dictionary<string, SceneGroup>();
    public Dictionary<string, CosmeticData> CosmeticDict = new Dictionary<string, CosmeticData>();

    public void OnEnable()
    {
        SceneGroup[] levelData = Resources.LoadAll<SceneGroup>("Datas/SceneGroups/Levels");
        CosmeticData[] cosmeticData = Resources.LoadAll<CosmeticData>("Datas/Cosmetics");

        foreach(SceneGroup level in levelData)
        {
            LevelsList.Add(level);
            LevelDict[level.Name] = level;
        }

        foreach(CosmeticData cosmetic in cosmeticData)
        {
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

//using System.Collections.Generic;
//using UnityEngine;

//// Contains the game data
//// Use the CharacterList if ordered indexing is needed.
//// Otherwise directly query cost by providing characterID.
//public static class SimpleRunnerGameData
//{
//    public static readonly List<RunnerCharacterData> CharacterList = new();

//    private static readonly Dictionary<string, RunnerCharacterData> CharacterData = new();

//    static SimpleRunnerGameData()
//    {
//        RunnerCharacterData[] data = Resources.LoadAll<RunnerCharacterData>("GameData/");

//        foreach (RunnerCharacterData datum in data)
//        {
//            CharacterData.Add(datum.Id, datum);
//            CharacterList.Add(datum);
//        }
//    }

//    public static Animator GetCharacterArt(string characterId)
//    {
//        if (CharacterData.TryGetValue(characterId, out RunnerCharacterData data))
//        {
//            return data.CharacterPrefab;
//        }
//        Debug.LogError($"GetCharacterArt: '{characterId}' not found!");
//        return null;
//    }

//    public static int GetCharacterCost(string characterId)
//    {
//        if (CharacterData.TryGetValue(characterId, out RunnerCharacterData data))
//        {
//            return data.Cost;
//        }

//        Debug.LogError($"GetCharacterCost: '{characterId}' not found!");
//        return 0;
//    }
//}