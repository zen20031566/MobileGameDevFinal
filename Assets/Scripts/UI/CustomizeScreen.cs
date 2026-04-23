using System.Collections.Generic;
using UnityEngine;

public class CustomizeScreen : ScreenBase
{
    [SerializeField] private GameData gameData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform cosmeticElementGroup;
    [SerializeField] private CosmeticUIElement cosmeticElementPrefab;
    private List<CosmeticUIElement> cosmeticElements = new List<CosmeticUIElement>();

    protected override void OnShow()
    {
        UpdateContents();
    }

    public void UpdateContents()
    {
        foreach (CosmeticUIElement element in cosmeticElements)
        {
            Destroy(element.gameObject);
        }
        cosmeticElements.Clear();

        CosmeticUIElement noneElement = Instantiate(cosmeticElementPrefab, cosmeticElementGroup); 
        noneElement.Init(gameData.GetCosmeticData("None"));
        noneElement.SetOwned(true);
        if (playerData.CurrentCosmetic == "None") noneElement.SetEquipped(true);

        foreach (CosmeticData cosmetic in gameData.CosmeticsList)
        {
            if (cosmetic.Id == "None") continue;

            CosmeticUIElement cosmeticElement = Instantiate(cosmeticElementPrefab, cosmeticElementGroup);
            cosmeticElement.Init(cosmetic);

            if (playerData.IsCosmeticOwned(cosmetic.Id)) cosmeticElement.SetOwned(true);
            if (playerData.CurrentCosmetic == cosmetic.Id) cosmeticElement.SetEquipped(true);
        }
    }
}
