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
    [SerializeField] private Button settingsButton;
    [SerializeField] private TMP_Text currencyText;

    [SerializeField] LevelSelect levelSelect;
    [SerializeField] ScreenBase customizeScreen;
    [SerializeField] ScreenBase settingsScreen;
    [SerializeField] ScreenBase creditsScreen;

    private void Start()
    {
        startButton.onClick.AddListener(HandleStartClicked);
        customizeButton.onClick.AddListener(HandleCustomizeClicked);
    }

    private void UpdateCurrency()
    {
        currencyText.text = playerData.Currency.ToString();
    }

    private void HandleStartClicked()
    {
        GameScreenManager.Push(levelSelect, "Menu");
    }

    private void HandleCustomizeClicked()
    {
        GameScreenManager.Push(customizeScreen, "Menu");
    }

    private void HandleSettingsClicked()
    {

    }

    private void HandleFourHrRewardClicked()
    {
        {

        }

    }
}
