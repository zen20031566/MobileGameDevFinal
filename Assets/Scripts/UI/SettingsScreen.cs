using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsScreen : ScreenBase
{
    [SerializeField] PlayerData playerData;
    [SerializeField] MenuManager menuManager;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button resetDataButton;

    [SerializeField] ConfirmationPopUp confirmationPopUp;

    private void Start()
    {
        closeButton.onClick.AddListener(() => GameScreenManager.Pop(gameObject.scene.name));
        resetDataButton.onClick.AddListener(HandleResetDataButtonClicked);
    }

    private void HandleResetDataButtonClicked()
    {
        UnityAction resetPlayerData = async () =>
        {
            playerData.ResetData();
            await menuManager.SceneLoader.LoadSceneGroup("Menu");
        };

        confirmationPopUp.Init(yesAction: resetPlayerData, customDisplayText: "DELETE PLAYER DATA?");
        GameScreenManager.Push(confirmationPopUp, gameObject.scene.name);
    }
}
