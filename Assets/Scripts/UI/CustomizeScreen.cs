using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomizeScreen : ScreenBase
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameData gameData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform cosmeticElementGroup;
    [SerializeField] private CosmeticUIElement cosmeticElementPrefab;
    private List<CosmeticUIElement> cosmeticElements = new List<CosmeticUIElement>();
    [SerializeField] MenuManager menuManager;
    [SerializeField] ConfirmationPopUp confirmationPopUp;
    [SerializeField] DialogPopUp dialogPopUp;
    [SerializeField] private TMP_Text currencyText;

    private void Start()
    {
        closeButton.onClick.AddListener(() => GameScreenManager.Pop(gameObject.scene.name));
        playerData.OnCurrencyChange += UpdateCurrency;
    }

    private void UpdateCurrency()
    {
        currencyText.text = playerData.Currency.ToString();
    }

    protected override void OnShow()
    {
        UpdateContents();
        UpdateCurrency();
    }

    public void UpdateContents()
    {
        foreach (CosmeticUIElement element in cosmeticElements)
        {
            Destroy(element.gameObject);
        }
        cosmeticElements.Clear();

        foreach (CosmeticData cosmetic in gameData.CosmeticsList)
        {
            CosmeticUIElement cosmeticElement = Instantiate(cosmeticElementPrefab, cosmeticElementGroup);
            cosmeticElement.Init(cosmetic);
            cosmeticElements.Add(cosmeticElement);

            //If owned
            if (playerData.IsCosmeticOwned(cosmetic.Id))
            {
                cosmeticElement.SetOwned(true);

                //Logic for equip
                cosmeticElement.Button.onClick.AddListener(() =>
                {
                    if (playerData.CurrentCosmetic != cosmetic.Id)
                    {
                        if (playerData.ChangeCosmetic(cosmetic.Id))
                        {
                            cosmeticElement.SetEquipped(true);
                            UpdateContents();
                            menuManager.SwitchCosmetic(cosmetic.Id);
                        }
                    }
                });
            }
            else //Not owned
            {
                cosmeticElement.SetOwned(false);
                cosmeticElement.Button.onClick.AddListener(() =>
                {
                    //If have money
                    if (playerData.Currency >= gameData.GetCosmeticData(cosmetic.Id).Cost)
                    {
                        UnityAction buyLogic = () =>
                        {
                            //Switch after buy
                            if (playerData.TryPurchaseCosmetic(cosmetic.Id))
                            {
                                playerData.ChangeCosmetic(cosmetic.Id);
                                UpdateContents();
                            }
                            GameScreenManager.Pop(gameObject.scene.name);
                        };

                        confirmationPopUp.Init(yesAction: buyLogic, customDisplayText: "BUY COSMETIC?");
                        GameScreenManager.Push(confirmationPopUp, gameObject.scene.name);
                    }
                    else //No money
                    {
                        dialogPopUp.SetDisplayText("NOT ENOUGH CURRENCY >:(");
                        GameScreenManager.Push(dialogPopUp, gameObject.scene.name);
                    }
                });
            }

            if (playerData.CurrentCosmetic == cosmetic.Id) cosmeticElement.SetEquipped(true);
        }
    }
}
