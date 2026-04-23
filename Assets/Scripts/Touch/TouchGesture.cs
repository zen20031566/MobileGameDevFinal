
using UnityEngine;

public class TouchGesture
{
    private const float MINIMUM_DISTSQR = 100.0f;

    // This Id matches Unity touchId for a given touch
    public int TouchId { get; private set; }

    // The time when the TouchGesture just began, equivalent to Time.realtimeSinceStartupAsDouble on that frame
    public double BeginTime { get; private set; }

    // The time when the TouchGesture just ended, equivalent to Time.realtimeSinceStartupAsDouble on that frame
    public double EndTime { get; private set; }

    // Time difference between SubmitPoint calls. Most likely not needed.
    public double DeltaTime { get; private set; }

    // How old the TouchGesture is since it began
    public double Age => Time.realtimeSinceStartupAsDouble - BeginTime;

    // If the TouchGesture has moved at least 10 pixels (hardcoded) it is considered moving
    public bool HasMoved { get; private set; }

    // The start location of the touch
    public Vector2 StartScreenPosition { get; private set; }

    // The current location of the touch.
    public Vector2 CurrentScreenPosition { get; private set; }

    // The previous location of the touch
    public Vector2 LastScreenPosition { get; private set; }

    // The offset between CurrentScreenPosition and LastScreenPosition
    public Vector2 DeltaScreenPosition { get; private set; }

    // The offset between CurrentScreenPosition and StartScreenPosition
    public Vector2 DeltaStartEndPosition { get; private set; }

    // The touch movement velocity, in pixels
    public Vector2 Velocity { get; private set; }

    // How much has touch moved from the beginning (in pixels)
    public float TravelDistance { get; private set; }

    // Likelihood that the touch movement matches the direction from StartPosition to LastPosition
    public float Sameness { get; private set; }

    private Vector2 CumulativeMoveDirection;
    private int SampleSize;

    public TouchGesture(int touchId, Vector2 position, double time)
    {
        TouchId = touchId;
        BeginTime = EndTime = time;
        StartScreenPosition = CurrentScreenPosition = LastScreenPosition = position;
    }

    public void SubmitPoint(Vector2 position, double time)
    {
        LastScreenPosition = CurrentScreenPosition;
        CurrentScreenPosition = position;
        DeltaScreenPosition = CurrentScreenPosition - LastScreenPosition;
        DeltaStartEndPosition = CurrentScreenPosition - StartScreenPosition;

        double lastTime = EndTime;
        EndTime = time;
        DeltaTime = EndTime - lastTime;

        Velocity = (CurrentScreenPosition - LastScreenPosition) / (float)(DeltaTime);

        if (!HasMoved)
        {
            HasMoved = (StartScreenPosition - CurrentScreenPosition).sqrMagnitude > MINIMUM_DISTSQR;
        }

        Vector2 offset = CurrentScreenPosition - StartScreenPosition;
        float length = offset.magnitude;
        offset /= length;
        TravelDistance += length;
        CumulativeMoveDirection += offset;
        SampleSize++;
        Sameness = Vector2.Dot(offset, CumulativeMoveDirection / SampleSize);
    }
}