using UnityEngine;
using UnityEngine.UI;

public class MainMenu : ScreenBase
{
    [SerializeField] MenuManager menuManager;

    [SerializeField] Button startButton;
    [SerializeField] Button customizeButton;
    [SerializeField] Button dailyLoginButton;
    [SerializeField] Button settingsButton;

    private void Start()
    {
        startButton.onClick.AddListener(HandleStartClicked);
    }

    private void UpdateCurrency()
    {

    }

    private async void HandleStartClicked()
    {
        await menuManager.SceneLoader.LoadSceneGroup("Game");
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
