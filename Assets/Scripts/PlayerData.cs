using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private GameData gameData;

    [SerializeField] private int currency = 0;
    [SerializeField] private string currentHat = "None";

    private static readonly Dictionary<string, bool> cosmeticCollection = new();

    public int Currency => currency;
    public string CurrentHat => currentHat;

    private void OnEnable()
    {
        //currency = PlayerPrefs.GetInt("Currency", 0);

        //foreach (var character in SimpleRunnerGameData.CharacterList)
        //{
        //    bool isOwned = bool.Parse(PlayerPrefs.GetString(character.Id, "false"));
        //    characterCollection.Add(character.Id, isOwned);
        //}

        //characterCollection["Cat"] = true;
        //currentCharacter = PlayerPrefs.GetString("CurrentCharacter", "Cat");

        //Debug.Log($"Loaded Player Data");
    }

    public void AddCurrency(int amount)
    {
        if (amount < 0) return;

        currency += amount;
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.Save();

        Debug.Log($"Added {amount} currency.");
    }

    //public static bool TryPurchaseCosmetic(string id)
    //{
    //    int cost = SimpleRunnerGameData.GetCharacterCost(characterId);

    //    if (cost < 0)
    //    {
    //        Debug.LogError($"Cannot purchase '{characterId}' !\nReason: It is free!");
    //        return false;
    //    }

    //    characterCollection.TryGetValue(characterId, out bool characterOwned);
    //    if (characterOwned)
    //    {
    //        Debug.LogError($"Cannot purchase '{characterId}' !\nReason: Already owned!");
    //        return false;
    //    }

    //    if (currency < cost)
    //    {
    //        Debug.LogError($"Cannot purchase '{characterId}' !\nReason: Not enough currency.");
    //        return false;
    //    }

    //    // deduct cost from currency
    //    // Save currency to disk
    //    // alter the character's entry in characterCollection to be 'true' value
    //    // Save state of characterId to disk

    //    currency -= cost;
    //    characterCollection[characterId] = true;
    //    PlayerPrefs.SetString(characterId, "true");
    //    PlayerPrefs.Save();

    //    Debug.Log($"Purchased '{characterId}' for {cost} currency");
    //    return true;
    //}

    //public static void ResetData()
    //{
    //    PlayerPrefs.DeleteAll();
    //    currency = 0;
    //    characterCollection.Clear();
    //    characterCollection.Add("Cat", true);
    //    currentCharacter = "Cat";
    //    Debug.Log("Player Data reset.");
    //}

    //public static bool IsCharacterOwned(string characterId)
    //{
    //    characterCollection.TryGetValue(characterId, out bool result);
    //    return result;
    //}

    //public static bool ChangeHat(string id)
    //{
    //    characterCollection.TryGetValue(id, out bool result);

    //    if (result)
    //    {
    //        string oldCharacter = currentCharacter;
    //        currentCharacter = id;
    //        Debug.Log($"Changed character from '{oldCharacter}' to '{id}'");
    //        PlayerPrefs.SetString("CurrentCharacter", id);
    //        PlayerPrefs.Save();
    //    }
    //    else
    //    {
    //        Debug.LogError($"Cannot change character from '{currentCharacter}' to '{id}'\nReason: Character not owned.");
    //    }

    //    return result;
    //}
}
