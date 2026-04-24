using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : ScreenBase
{
    [SerializeField] private Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(() => GameScreenManager.Pop(gameObject.scene.name));
    }
}
