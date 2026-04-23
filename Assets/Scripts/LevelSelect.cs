using UnityEngine;

public class LevelSelect : ScreenBase
{
    [SerializeField] private GameData gameData;

    protected override void OnShow()
    {
        foreach (CosmeticData data in gameData.CosmeticsList)
        {

        }
    }
}
