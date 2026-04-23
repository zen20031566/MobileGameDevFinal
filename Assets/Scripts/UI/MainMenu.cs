using UnityEngine;
using UnityEngine.UI;

public class MainMenu : ScreenBase
{
    [SerializeField] MenuManager menuManager;

    [SerializeField] Button startButton;
    [SerializeField] Button customizeButton;
    [SerializeField] Button dailyLoginButton;
    [SerializeField] Button settingsButton;

    [SerializeField] LevelSelect levelSelect;

    private void Start()
    {
        startButton.onClick.AddListener(HandleStartClicked);
    }

    private void UpdateCurrency()
    {

    }

    private async void HandleStartClicked()
    {
        GameScreenManager.Push(levelSelect, "Menu");
    }

    private void HandleCustomizeClicked()
    {

    }

    private void HandleSettingsClicked()
    {

    }

    private void HandleDailyLoginClicked()
    {

    }

}
