using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneGroup[] sceneGroups;

    private float targetProgress = 0f;
    private bool isLoading = false;

    [SerializeField] private Camera loadingCamera;
    [SerializeField] private LoadingScreen loadingScreen;

    public readonly SceneGroupManager sceneGroupManager = new SceneGroupManager();

    async private void Start()
    {
        await LoadSceneGroup(0);
    }

    private void Update()
    {
        if (!isLoading) return;


        float currentFillAmount = loadingScreen.LoadingBar.fillAmount;
        float progressDif = Mathf.Abs(currentFillAmount - targetProgress);

        float dynamicFillSpeed = progressDif * loadingScreen.FillSpeed;
        loadingScreen.LoadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime * dynamicFillSpeed);
    }

    public async Task LoadSceneGroup(int index)
    {
        loadingScreen.LoadingBar.fillAmount = 0f;
        targetProgress = 1f;

        if (index < 0 || index >= sceneGroups.Length)
        {
            Debug.LogError(this + " Invalid scene group index " + index);
            return;
        }

        LoadingProgress loadingProgress = new LoadingProgress();
        loadingProgress.OnProgress += target => targetProgress = Mathf.Max(target, targetProgress);

        isLoading = true;
        loadingCamera.gameObject.SetActive(true);
        GameScreenManager.Push(loadingScreen, gameObject.scene.name);
        await sceneGroupManager.LoadScenes(sceneGroups[index], loadingProgress);
        isLoading = false;
        loadingCamera.gameObject.SetActive(false);
        GameScreenManager.Pop(gameObject.scene.name);
    }

    public async Task LoadSceneGroup(string name)
    {
        loadingScreen.LoadingBar.fillAmount = 0f;
        targetProgress = 1f;

        var group = Array.Find(sceneGroups, g => g.Name == name);

        if (group == null)
        {
            Debug.LogError(this + " Invalid scene group name: " + name);
            return;
        }

        LoadingProgress loadingProgress = new LoadingProgress();
        loadingProgress.OnProgress += target => targetProgress = Mathf.Max(target, targetProgress);

        isLoading = true;

        loadingCamera.gameObject.SetActive(true);
        GameScreenManager.Push(loadingScreen, gameObject.scene.name);
        await sceneGroupManager.LoadScenes(group, loadingProgress);
        isLoading = false;
        loadingCamera.gameObject.SetActive(false);
        GameScreenManager.Pop(gameObject.scene.name);
    }
}

public class LoadingProgress : IProgress<float>
{
    public event Action<float> OnProgress;

    const float ratio = 1f;

    public void Report(float value)
    {
        OnProgress?.Invoke(value / ratio);
    }
}
