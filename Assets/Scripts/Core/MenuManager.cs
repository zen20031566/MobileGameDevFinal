using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public SceneLoader SceneLoader {  get; private set; }
    public UnityAdsManager UnityAdsManager { get; private set; }

    private void Start()
    {
        SceneLoader = FindAnyObjectByType<SceneLoader>();
        if (SceneLoader == null) Debug.LogError(this + " Scene loader cannot be found");

        UnityAdsManager = FindAnyObjectByType<UnityAdsManager>();
        if (UnityAdsManager == null) Debug.LogError(this + " Unity ads manager cannot be found");
    }

    public void SwitchHat()
    {

    }

}
