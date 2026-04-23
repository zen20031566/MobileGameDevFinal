using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public SceneLoader SceneLoader {  get; private set; }

    private void Start()
    {
        SceneLoader = FindAnyObjectByType<SceneLoader>();
        if (SceneLoader == null) Debug.LogError(this + " Scene loader cannot be found");
    }

}
