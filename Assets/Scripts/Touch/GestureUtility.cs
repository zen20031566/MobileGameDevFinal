using UnityEngine;

public class GestureUtility
{
    public static bool CheckLongPress(TouchGesture gesture, float durationToPass)
    {
        if (gesture == null || gesture.HasMoved) return false;
        return gesture.Age >= Mathf.Max(durationToPass, 0.0f);
    }
}