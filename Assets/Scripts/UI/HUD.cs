using UnityEngine;
using UnityEngine.UI;

public class HUD : ScreenBase
{
    [SerializeField] Button pauseButton;
    [SerializeField] PauseScreen pauseScreen;

    private void Start()
    {
        pauseButton.onClick.AddListener(() => GameScreenManager.Push(pauseScreen, gameObject.scene.name));
    }

}
