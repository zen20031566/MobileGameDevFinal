using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : ScreenBase
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Button returnButton;
    [SerializeField] Button closeButton;

    private void Start()
    {
        returnButton.onClick.AddListener(() => HandleReturnClicked());
        closeButton.onClick.AddListener(() => GameScreenManager.Pop(gameObject.scene.name));

    }

    public async void HandleReturnClicked()
    {
        await gameManager.SceneLoader.LoadSceneGroup("Menu");
    }
}
