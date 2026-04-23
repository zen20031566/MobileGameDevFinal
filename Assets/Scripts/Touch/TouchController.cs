using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

// Make this component have higher priority, but lower than EventSystem (-1000)
[DefaultExecutionOrder(-900)]
public class TouchController : MonoBehaviour
{
    private const float SAMENESS_CONSTANT = 0.95f;
    private const float MINIMUM_TRAVELDIST_CM = 2.54f; // 1 inch

    [SerializeField] private float tapDuration = 0.2f;
    [SerializeField] private float swipeDuration = 0.3f;
    //[SerializeField] private float longPressDuration = 1.0f;

    public float TapDuration
    {
        get => tapDuration; set => Mathf.Max(value, 0.1f);
    }

    public float SwipeDuration
    {
        get => swipeDuration; set => Mathf.Max(value, 0.3f);
    }

    //public float LongPressDuration
    //{
    //    get => longPressDuration; set => Mathf.Max(value, tapDuration);
    //}

    [Serializable]
    public class MyTouchEvent : UnityEvent<TouchGesture>
    { }

    private static TouchController Instance;

    public static MyTouchEvent onPress = new MyTouchEvent();
    public static MyTouchEvent onRelease = new MyTouchEvent();
    public static MyTouchEvent onTap = new MyTouchEvent();
    public static MyTouchEvent onMove = new MyTouchEvent();
    public static MyTouchEvent onLongPress = new MyTouchEvent();
    public static MyTouchEvent onSwipe = new MyTouchEvent();

    private Dictionary<int, TouchGesture> activeGestures = new Dictionary<int, TouchGesture>();

    public static void Activate()
    {
        if (Instance == null)
        {
            GameObject go = new("TouchController");
            Instance = go.AddComponent<TouchController>();
            DontDestroyOnLoad(go);
        }
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        var activeTouches = Touch.activeTouches;

        foreach (var touch in activeTouches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                var gesture = new TouchGesture(touch.touchId, touch.startScreenPosition, Time.realtimeSinceStartupAsDouble);
                activeGestures.Add(touch.touchId, gesture);
                onPress.Invoke(gesture);
            }
            else
            {
                if (activeGestures.TryGetValue(touch.touchId, out var gesture))
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Moved:
                            {
                                gesture.SubmitPoint(touch.screenPosition, Time.realtimeSinceStartupAsDouble);
                                onMove.Invoke(gesture);
                            };
                            break;

                        //case TouchPhase.Stationary:
                        //    {
                        //        if (GestureUtility.CheckLongPress(gesture, longPressDuration))
                        //        {
                        //            onLongPress.Invoke(gesture);

                        //            // Remove the gesture after long press activates
                        //            activeGestures.Remove(touch.touchId);
                        //        }
                        //    }
                        //    break;

                        case TouchPhase.Ended:
                        case TouchPhase.Canceled:
                            {
                                HandleTouchEnded(gesture, touch);
                                activeGestures.Remove(touch.touchId);
                            }
                            break;
                    }
                }
            }
        }
    }

    private void HandleTouchEnded(TouchGesture gesture, Touch touch)
    {
        gesture.SubmitPoint(touch.screenPosition, Time.realtimeSinceStartupAsDouble);

        onRelease.Invoke(gesture);

        if (gesture.HasMoved)
        {
            if (gesture.Age < swipeDuration && gesture.Sameness > SAMENESS_CONSTANT)
            {
                // Different mobile devices have different pixel density, therefore movement in pixels
                // is not useful metric.
                // Dividing pixel by DPI provides us physical measurement (in inches)
                float swipeLengthInch = gesture.TravelDistance / Screen.dpi;

                if (swipeLengthInch > MINIMUM_TRAVELDIST_CM) // by default 1 inch, or 2.54cm
                {
                    onSwipe.Invoke(gesture);
                }
            }
        }
        else
        {
            if (gesture.Age < tapDuration)
            {
                onTap.Invoke(gesture);
            }
        }
    }
}