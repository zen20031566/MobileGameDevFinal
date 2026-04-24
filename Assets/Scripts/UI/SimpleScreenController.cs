using System.Collections.Generic;
using UnityEngine;

public class SimpleScreenController : MonoBehaviour
{
    private string id;

    // The first screen to show
    [SerializeField] private ScreenBase startingScreen;

    // If true, the starting screen will instantly be shown
    [SerializeField] private bool instantlyShowStartingScreen = false;

    private readonly List<ScreenBase> screens = new();

    private void Awake()
    {
        id = gameObject.scene.name;
        GameScreenManager.Register(this, id);
    }

    private void OnDestroy() => GameScreenManager.Unregister(id);

    private void Start()
    {
        if (startingScreen != null)
        {
            Push(startingScreen, instantlyShowStartingScreen);
        }
    }

    public void Push(ScreenBase newScreen, bool instant = false)
    {
        if (screens.Count > 0)
        {
            ScreenBase current = screens[^1];
            current.Unfocus();
        }

        screens.Add(newScreen);
        newScreen.Show(instant);
    }

    public void Pop(bool instant = false)
    {
        if (screens.Count == 0)
        {
            Debug.LogWarning("Pop called but screen stack is empty.");
            return;
        }

        ScreenBase current = screens[^1];
        current.Hide(instant);
        screens.RemoveAt(screens.Count - 1);

        if (screens.Count > 0)
        {
            current = screens[^1];
            current.Focus();
        }
    }

    private void Update()
    {
        if (GameScreenManager.ActiveController != this) return;
        if (UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame )
        {
            if (screens.Count > 1)
            {
                ScreenBase current = screens[^1];
                if (current != null && current.ShouldHonorBackButton())
                {
                    Pop();
                }
            }
        }
    }

#if UNITY_EDITOR

    private void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.fontSize = 36;
        fontStyle.normal.textColor = Color.white;

        GUILayout.BeginVertical();

        GUILayout.Label("SimpleScreenManager [Editor DebugView]", fontStyle);
        GUILayout.Label("Screens:", fontStyle);
        for (int i = 0; i < screens.Count; i++)
        {
            bool isLast = i == screens.Count - 1;
            fontStyle.normal.textColor = isLast ? Color.green : Color.white;

            var screen = screens[i];
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUILayout.Label($"[{i}] {screen.name} {(isLast ? "<--" : "")}", fontStyle);
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
    }

#endif 
}