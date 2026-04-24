using UnityEngine;

[CreateAssetMenu(fileName = "CosmeticData", menuName = "Scriptable Objects/CosmeticData")]
public class CosmeticData : ScriptableObject
{
    public string Id;
    public GameObject Prefab;
    public int Cost;
    public Texture2D Image;
}
