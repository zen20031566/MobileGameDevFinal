using UnityEngine;
using UnityEngine.TextCore.Text;

public class MenuManager : MonoBehaviour
{
    public SceneLoader SceneLoader {  get; private set; }
    public UnityAdsManager UnityAdsManager { get; private set; }

    [SerializeField] PlayerData playerData;
    [SerializeField] GameData gameData;
    [SerializeField] private ObjScreenshotter objScreenshotter;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform cosmeticSpawnPoint;

    private GameObject currentEquippedCosmetic = null;

    private void Awake()
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

        playerData.OnCurrentCosmeticChange += SwitchCosmetic;
        SwitchCosmetic(playerData.CurrentCosmetic);

        //UnityAdsManager.On
    }

    private void OnDisable()
    {
        playerData.OnCurrentCosmeticChange -= SwitchCosmetic;
    }

    public void SwitchCosmetic(string id)
    {
        var prefab = gameData.GetCosmeticData(id).Prefab;

        if (cosmeticSpawnPoint.childCount > 0)
        {
            foreach (Transform child in cosmeticSpawnPoint)
            {
                Destroy(child.gameObject);
            }
        }

        if (id != "None") currentEquippedCosmetic = Instantiate(prefab, cosmeticSpawnPoint);
    }
}
