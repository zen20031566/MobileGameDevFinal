using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGroupManager 
{
    public event Action<string> OnSceneLoaded;
    public event Action<string> OnSceneUnloaded;
    public event Action OnSceneGroupLoaded;

    private SceneGroup activeSceneGroup;

    public async Task LoadScenes(SceneGroup sceneGroup, IProgress<float> progress)
    {
        activeSceneGroup = sceneGroup;
        List<string> loadedScenes = new List<string>();

        await UnloadScenes();

        int sceneCount = SceneManager.sceneCount;

        //Add core scenes, scenes which are not in the scene groups like the bootstrapper or like initialization stuff
        for (int i = 0; i < sceneCount; i++)
        {
            loadedScenes.Add(SceneManager.GetSceneAt(i).name);
        }

        //Load the scenes in the scene group
        int totalScenesToLoad = activeSceneGroup.Scenes.Count;
        AsyncOperationGroup operationGroup = new AsyncOperationGroup(totalScenesToLoad);

        for (int i = 0;i < totalScenesToLoad; i++)
        {
            string scene = sceneGroup.Scenes[i];
            if (loadedScenes.Contains(scene)) continue; //prevent duplicate scenes

            var operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            operationGroup.Operations.Add(operation);

            OnSceneLoaded?.Invoke(scene);
        }

        //Wait until all operations are done
        while (!operationGroup.IsDone)
        {
            progress?.Report(operationGroup.Progress);
            await Task.Delay(100);
        }

        Scene activeScene = SceneManager.GetSceneByName(activeSceneGroup.ActiveScene);

        if (activeScene.IsValid())
        {
            SceneManager.SetActiveScene(activeScene);
        }
        else
        {
            Debug.LogError(sceneGroup.Name + " Active scene not valid");
        }

        OnSceneGroupLoaded?.Invoke();
    }

    public async Task UnloadScenes()
    {
        List<string> scenes = new List<string>();
        //string activeScene = SceneManager.GetActiveScene().name;
        int sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (!scene.isLoaded) continue;

            if (scene.name == "Bootstrapper") continue;
            scenes.Add(scene.name);
        }

        AsyncOperationGroup operationGroup = new AsyncOperationGroup(sceneCount);

        foreach (string scene in scenes)
        {
            var operation = SceneManager.UnloadSceneAsync(scene);   
            if (operation == null) continue;

            operationGroup.Operations.Add(operation);

            OnSceneUnloaded?.Invoke(scene);
        }

        //Wait until all operations are done
        while (!operationGroup.IsDone)
        {
            await Task.Delay(100);
        }
    }

  
}

public readonly struct AsyncOperationGroup
{
    public readonly List<AsyncOperation> Operations;

    public AsyncOperationGroup(int initialCapacity)
    {
        Operations = new List<AsyncOperation>(initialCapacity);
    }

    public float Progress => Operations.Count == 0 ? 0f : Operations.Average(o => o.progress);
    public bool IsDone => Operations.All(o => o.isDone);
}
