using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogPopUp : ScreenBase
{
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private Button closeButton;

    private string defaultDisplayText = "Yo";

    private void Start()
    {
        displayText.text = defaultDisplayText;
        closeButton.onClick.AddListener(() => GameScreenManager.Pop(gameObject.scene.name));
    }

    protected override void OnHide()
    {
        displayText.text = defaultDisplayText;
    }

    public void SetDisplayText(string text)
    {
        displayText.text = text;
    }
}
