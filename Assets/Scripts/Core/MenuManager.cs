using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public SceneLoader SceneLoader {  get; private set; }
    public UnityAdsManager UnityAdsManager { get; private set; }

    [SerializeField] GameData gameData;
    [SerializeField] private ObjScreenshotter objScreenshotter;

    private void Start()
    {
        SceneLoader = FindAnyObjectByType<SceneLoader>();
        if (SceneLoader == null) Debug.LogError(this + " Scene loader cannot be found");

        UnityAdsManager = FindAnyObjectByType<UnityAdsManager>();
        if (UnityAdsManager == null) Debug.LogError(this + " Unity ads manager cannot be found");

        foreach(CosmeticData cosmetic in gameData.CosmeticsList)
        {
            if (cosmetic.Id == "None") continue;
            Texture2D screenshot = objScreenshotter.InstantiateAndScreenshot(cosmetic.Prefab);
            cosmetic.Image = screenshot;
        }
    }

    public void SwitchHat()
    {

    }

}
