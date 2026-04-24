using UnityEngine;

public class PlayerInputControls : MonoBehaviour
{
    private bool startedOnBall;
    private bool isDirty;
    private readonly TouchGesture[] trackedGestures = new TouchGesture[2] { null, null };

    private void OnEnable()
    {
        TouchController.onPress.AddListener(OnPress);
        TouchController.onRelease.AddListener(OnRelease);
        TouchController.onMove.AddListener(OnMove);
    }

    private void OnDisable()
    {
        TouchController.onPress.RemoveListener(OnPress);
        TouchController.onRelease.RemoveListener(OnRelease);
        TouchController.onMove.RemoveListener(OnMove);
    }

    private void OnPress(TouchGesture gesture)
    {

    }

    private void OnRelease(TouchGesture gesture)
    {

    }

    private void OnMove(TouchGesture gesture)
    {

    }
}
