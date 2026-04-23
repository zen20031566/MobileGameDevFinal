using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : ScreenBase
{
    public Image LoadingBar;
    [SerializeField] private float fillSpeed = 0.5f;
    public float FillSpeed => fillSpeed;

}
