using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScreen : ScreenBase
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Button returnButton;

    private void Start()
    {
        returnButton.onClick.AddListener(() => HandleReturnClicked());
     

    }

    public async void HandleReturnClicked()
    {
        await gameManager.SceneLoader.LoadSceneGroup("Menu");
    }
}
