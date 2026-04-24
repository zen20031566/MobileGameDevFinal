using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : ScreenBase
{
    [SerializeField] MenuManager menuManager;
    [SerializeField] PlayerData playerData;

    [SerializeField] private Button startButton;
    [SerializeField] private Button customizeButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button fourHrRewardButton;
    [SerializeField] private Button watchAdsButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private TMP_Text currencyText;

    [SerializeField] LevelSelect levelSelect;
    [SerializeField] ScreenBase customizeScreen;
    [SerializeField] ScreenBase settingsScreen;
    [SerializeField] ScreenBase creditsScreen;
    [SerializeField] ConfirmationPopUp confirmationPopUp;
    [SerializeField] DialogPopUp dialogPopUp;

    private void Start()
    {
        menuManager.UnityAdsManager.ToggleBanner();

        startButton.onClick.AddListener(() => GameScreenManager.Push(levelSelect, gameObject.scene.name));
        customizeButton.onClick.AddListener(() => GameScreenManager.Push(customizeScreen, gameObject.scene.name));
        creditsButton.onClick.AddListener(() => GameScreenManager.Push(creditsScreen, gameObject.scene.name));

        watchAdsButton.onClick.AddListener(HandleWatchAdsButton);
        fourHrRewardButton.onClick.AddListener(HandleFourHrRewardClicked);
        settingsButton.onClick.AddListener(() => GameScreenManager.Push(settingsScreen, gameObject.scene.name));

        playerData.OnCurrencyChange += UpdateCurrency;
        UpdateCurrency();
    }

    private void OnDisable()
    {
        playerData.OnCurrencyChange -= UpdateCurrency;
    }

    private void UpdateCurrency()
    {
        currencyText.text = playerData.Currency.ToString();
    }

    private void HandleFourHrRewardClicked()
    {
        if (playerData.IsEligableForFourHrReward(out TimeSpan timeLeft))
        {
            dialogPopUp.SetDisplayText("Claimed 67 coins");
            GameScreenManager.Push(dialogPopUp, gameObject.gameObject.scene.name);
            playerData.AddCurrency(67);
            Debug.Log("4hr reward claimed");
        }
        else
        {
            string formatted = $"{timeLeft.Hours}h {timeLeft.Minutes}m {timeLeft.Seconds}s";
            dialogPopUp.SetDisplayText("Reward not ready come back in " + formatted);
            GameScreenManager.Push(dialogPopUp, gameObject.gameObject.scene.name);
        }
    }

    private void HandleWatchAdsButton()
    {
        menuManager.UnityAdsManager.LoadRewardedAd();
        menuManager.UnityAdsManager.ShowRewardedAd();
    }
}
