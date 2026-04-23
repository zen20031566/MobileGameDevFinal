using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : ScreenBase
{
    [SerializeField] MenuManager menuManager;
    [SerializeField] private GameData gameData;
    private List<SceneGroup> levels = new List<SceneGroup>();
    [SerializeField] Button levelButtonPrefab;
    [SerializeField] private Transform levelLayoutGroup;

    private void Start()
    {
        foreach (SceneGroup level in gameData.LevelsList)
        {
            Button levelButton = Instantiate(levelButtonPrefab, levelLayoutGroup);
            TMP_Text text = levelButton.GetComponentInChildren<TMP_Text>();

            if (text != null) text.text = level.Name;
            levelButton.onClick.AddListener(() => LoadLevel(level.Name));
        }
    }

    public async void LoadLevel(string levelName)
    {
        await menuManager.SceneLoader.LoadSceneGroup(levelName);
    }


}
