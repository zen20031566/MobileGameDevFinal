using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject playerPrefab;

    public SceneLoader SceneLoader { get; private set; }
    public UnityAdsManager UnityAdsManager { get; private set; }

    public Ball Ball { get; private set; } 

    private Transform startPoint;
    private Transform hole;
    private Transform bounds;

    [SerializeField] LevelCompleteScreen levelCompleteScreen;

    private int shots = 0;

    private void Awake()
    {
        //idc
        SceneLoader = FindAnyObjectByType<SceneLoader>();
        if (SceneLoader == null) Debug.LogError(this + " Scene loader cannot be found");

        UnityAdsManager = FindAnyObjectByType<UnityAdsManager>();
        if (UnityAdsManager == null) Debug.LogError(this + " Unity ads manager cannot be found");

        startPoint = GameObject.FindWithTag("StartPoint").transform;
        if (startPoint == null) Debug.LogError(SceneManager.GetActiveScene().name + " start point not set");

        hole = GameObject.FindWithTag("Hole").transform;
        if (startPoint == null) Debug.LogError(SceneManager.GetActiveScene().name + " hole not set");

        bounds = GameObject.FindWithTag("Bounds").transform;
        if (bounds == null) Debug.LogError(SceneManager.GetActiveScene().name + " bounds not set");

        GameObject player = Instantiate(playerPrefab, startPoint);
        Ball = player.GetComponentInChildren<Ball>();
        if (Ball == null) Debug.LogError(SceneManager.GetActiveScene().name + " NO BALL??");
        CosmeticData currentCosmetic = gameData.GetCosmeticData(playerData.CurrentCosmetic);
        if (currentCosmetic.Prefab !=null) Instantiate(currentCosmetic.Prefab, Ball.CosmeticSpawnPoint);
        
        Ball.OnEnterHole += GameEnd;
        Ball.OnShot += () => shots++;
    }

    private void OnDisable()
    {
        if (Ball != null)
        {
            Ball.OnEnterHole -= GameEnd;
            Ball.OnShot -= () => shots++;
        }
    }

    public void GameEnd()
    {
        UnityAdsManager.LoadNonRewardedAd();
        UnityAdsManager.ShowNonRewardedAd();
        GameScreenManager.Push(levelCompleteScreen, gameObject.scene.name);
        playerData.AddCurrency(67);
        //await SceneLoader.LoadSceneGroup("Menu");
    }
}
