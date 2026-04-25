using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputControls : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private bool startedOnBall;
    private bool isDirty;
    TouchGesture trackedGesture = null;
    private Ball ball;
    private float shotForce = 0f;
    [SerializeField] float verticalLookSpeed = 0.05f;
    [SerializeField] float horizontalLookSpeed = 0.1f;
    private float yaw = 0f;
    private float pitch = 0f;

    private void Start()
    {
        ball = gameManager.Ball;
    }

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

    private void Update()
    {
        if (ball.CurrentState != BallState.IDLE) return;

        if (isDirty && trackedGesture != null)
        {
            Vector2 delta = trackedGesture.CurrentScreenPosition - trackedGesture.LastScreenPosition;

            if (startedOnBall)
            {
                shotForce = (trackedGesture.StartScreenPosition.y - trackedGesture.CurrentScreenPosition.y) / (Screen.height * 0.1f) * ball.MaxShotForce;
                shotForce = Mathf.Clamp(shotForce, 0f, ball.MaxShotForce);
                ball.ShowAimArrow(shotForce);
            }
            else
            {
                //yaw += delta.x * verticalLookSpeed;
                //pitch += delta.y * horizontalLookSpeed;
                //pitch = Mathf.Clamp(pitch, 5f, 40f);

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    yaw += delta.x * horizontalLookSpeed;
                }
                else
                {
                    pitch += delta.y * verticalLookSpeed;
                    pitch = Mathf.Clamp(pitch, 5f, 40f);
                }

                ball.FollowCameraTarget.rotation = Quaternion.Euler(pitch, yaw, 0f);

                //Do this instead of transform.rotate cause gimbal??? idk
                ball.FollowCameraTarget.rotation = Quaternion.Euler(pitch, yaw, 0f);
            }

            isDirty = false;
        }
    }

    private void OnPress(TouchGesture gesture)
    {
        if (EventSystem.current.IsPointerOverGameObject(gesture.TouchId))
            return;

        if (trackedGesture == null)
        {
            trackedGesture = gesture;
        }

        Ray ray = Camera.main.ScreenPointToRay(gesture.CurrentScreenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<Ball>(out Ball ball))
            {
                startedOnBall = true;
            }
        }
        else
        {
            startedOnBall = false;
        }
    }

    private void OnRelease(TouchGesture gesture)
    {
        if (gesture == trackedGesture)
        {
            trackedGesture = null;
        }

        //Shoot
        if (startedOnBall && ball != null)
        {
            ball.HideAimArrow();

            if (shotForce > ball.MinShotForce) ball.Shoot(shotForce);
          
            shotForce = 0f;
            startedOnBall = false;
        }
    }

    private void OnMove(TouchGesture gesture)
    {
        if (gesture == trackedGesture)
        {
            isDirty = true;
        }
    }
}
