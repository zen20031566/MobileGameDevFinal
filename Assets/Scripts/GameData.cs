using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public List<SceneGroup> Levels = new List<SceneGroup>();
    public List<CosmeticData> CosmeticsList = new List<CosmeticData>();
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