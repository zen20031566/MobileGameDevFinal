using UnityEngine;

public enum BallState
{
    SPAWNING, IDLE, MOVING, INHOLE
}

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform followCameraTarget;

    public Transform FollowCameraTarget => followCameraTarget;
    public Rigidbody Rb { get; private set; }
    public Collider Col { get; private set; }

    public BallState CurrentState { get; private set; }

    private Vector3 lastShotPos;

    private Quaternion yawOnly;
    public Vector3 CameraFoward { get; private set; }

    private LineRenderer aimArrow;
    [SerializeField] private float minShotForce = 5f;
    [SerializeField] private float maxShotForce = 20f;
    [SerializeField] private float maxArrowLength = 1.5f;

    public float MinShotForce => minShotForce;
    public float MaxShotForce => maxShotForce;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();
        lastShotPos = transform.position;
        aimArrow = GetComponent<LineRenderer>();
        CurrentState = BallState.SPAWNING;
    }

    private void Update()
    {
        followCameraTarget.transform.position = transform.position;
        yawOnly = Quaternion.Euler(0f, followCameraTarget.eulerAngles.y, 0f);
        CameraFoward = yawOnly * Vector3.forward;

        if (Rb.linearVelocity.magnitude < 0.1 && CurrentState == BallState.MOVING)
        {
            CurrentState = BallState.IDLE;
            Debug.Log("Ball idle");
        }
    }

    public void ShowAimArrow(float shotForce)
    {
        float arrowLength = (shotForce / maxShotForce) * maxArrowLength;
        aimArrow.positionCount = 2;
        aimArrow.SetPosition(0, transform.position);
        aimArrow.SetPosition(1, FollowCameraTarget.position + CameraFoward * arrowLength);
        aimArrow.enabled = true;
    }

    public void HideAimArrow()
    {
        aimArrow.enabled = false;
    }

    public void Shoot(float shotForce)
    {
        lastShotPos = transform.position;
        Rb.AddForce(CameraFoward * shotForce, ForceMode.Impulse);
        CurrentState = BallState.MOVING;
        Debug.Log("Ball moving");
    }

    

}
