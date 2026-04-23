using System.Collections.Generic;
using UnityEngine;

public static class GameScreenManager
{
    private static Dictionary<string, SimpleScreenController> controllers = new Dictionary<string, SimpleScreenController>();
    public static SimpleScreenController ActiveController { private set; get; }

    public static void Register(SimpleScreenController controller, string id)
    {
        if (!controllers.ContainsKey(id))
        {
            controllers.Add(id, controller);
        }
    }

    public static void Unregister(string id)
    {
        if (!controllers.ContainsKey(id))
            return;

        controllers.Remove(id);
    }

    public static void SetActive(string id)
    {
        if (controllers.TryGetValue(id, out var controller))
        {
            ActiveController = controller;
        }
        else
        {
            Debug.Log($"No controller found with id {id}");
        }
    }

    public static void Push(ScreenBase screen, string id)
    {
        if (!controllers.ContainsKey(id))
        {
            Debug.Log(id + " SimpleScreenController not registered!");
            return;
        }

        controllers[id].Push(screen);
    }

    public static void Pop(string id)
    {
        if (!controllers.ContainsKey(id))
        {
            Debug.Log(id + " SimpleScreenController not registered!");
            return;
        }

        controllers[id].Pop();
    }
}