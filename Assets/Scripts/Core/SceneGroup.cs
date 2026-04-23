using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneGroup", menuName = "Scriptable Objects/SceneGroup")]
public class SceneGroup : ScriptableObject
{
    public string Name;
    public List<string> Scenes = new List<string>();
    public string ActiveScene;
}
