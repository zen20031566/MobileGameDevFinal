using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmationPopUp : ScreenBase
{
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private string defaultDisplayText = "CONFIRM????";

    private void Start()
    {
        displayText.text = defaultDisplayText;
    }

    protected override void OnHide()
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() => GameScreenManager.Pop(gameObject.scene.name));
        noButton.onClick.AddListener(() => GameScreenManager.Pop(gameObject.scene.name));
        displayText.text = defaultDisplayText;
    }

    public void Init(UnityAction yesAction = null, UnityAction noAction = null, string customDisplayText = null)
    {
        if (yesAction != null)
        {
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(yesAction);
        }

        if (noAction != null)
        {
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(noAction);
        }

        displayText.text = customDisplayText ?? defaultDisplayText;
    }
}
