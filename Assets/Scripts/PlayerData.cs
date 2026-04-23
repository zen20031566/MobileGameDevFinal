using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private GameData gameData;

    [SerializeField] private int currency = 0;
    [SerializeField] private string currentCosmetic = "None";

    private Dictionary<string, bool> cosmeticCollection;

    public int Currency => currency;
    public string CurrentCosmetic => currentCosmetic;

    private void OnEnable()
    {
        cosmeticCollection = new Dictionary<string, bool>();

        currency = PlayerPrefs.GetInt("Currency", 0);

        foreach (CosmeticData cosmetic in gameData.CosmeticsList)
        {
            bool isOwned = bool.Parse(PlayerPrefs.GetString(cosmetic.Id, "false"));
            cosmeticCollection.Add(cosmetic.Id, isOwned);
        }

        cosmeticCollection["None"] = true;
        currentCosmetic = PlayerPrefs.GetString("CurrentCosmetic", "None");

        Debug.Log($"Loaded Player Data");
    }

    public void AddCurrency(int amount)
    {
        if (amount < 0) return;

        currency += amount;
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.Save();

        Debug.Log($"Added {amount} currency.");
    }

    public bool TryPurchaseCosmetic(string id)
    {
        int cost = gameData.GetCosmeticData(id).Cost;

        if (cost < 0)
        {
            Debug.LogError($"Cannot purchase '{id}' !\nReason: It is free!");
            return false;
        }

        cosmeticCollection.TryGetValue(id, out bool owned);
        if (owned)
        {
            Debug.LogError($"Cannot purchase '{id}' !\nReason: Already owned!");
            return false;
        }

        if (currency < cost)
        {
            Debug.LogError($"Cannot purchase '{id}' !\nReason: Not enough currency.");
            return false;
        }

        currency -= cost;
        cosmeticCollection[id] = true;
        PlayerPrefs.SetString(id, "true");
        PlayerPrefs.Save();

        Debug.Log($"Purchased '{id}' for {cost} currency");
        return true;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        currency = 0;
        cosmeticCollection.Clear();
        cosmeticCollection.Add("None", true);
        currentCosmetic = "None";
        Debug.Log("Player Data reset.");
    }

    public bool IsCosmeticOwned(string id)
    {
        cosmeticCollection.TryGetValue(id, out bool result);
        return result;
    }

    public bool ChangeCosmetic(string id)
    {
        cosmeticCollection.TryGetValue(id, out bool result);

        if (result)
        {
            string oldCosmetic = currentCosmetic;
            currentCosmetic = id;
            Debug.Log($"Changed cosmetic from '{oldCosmetic}' to '{id}'");
            PlayerPrefs.SetString("CurrentCosmetic", id);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError($"Cannot change character from '{currentCosmetic}' to '{id}'\nReason: Cosmetic not owned.");
        }

        return result;
    }
}
