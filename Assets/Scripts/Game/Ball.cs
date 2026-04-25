using UnityEngine;
using System;

public enum BallState
{
    IDLE, MOVING, INHOLE
}

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform cosmeticSpawnPoint;
    public Transform CosmeticSpawnPoint => cosmeticSpawnPoint;

    [SerializeField] private Transform followCameraTarget;

    public Transform FollowCameraTarget => followCameraTarget;
    public Rigidbody Rb { get; private set; }
    public Collider Col { get; private set; }

    public BallState CurrentState { get; private set; }

    private Vector3 lastShotPos;

    private Quaternion yawOnly;
    public Vector3 CameraFoward { get; private set; }

    private LineRenderer aimArrow;
    [SerializeField] private float minShotForce = 1f;
    [SerializeField] private float maxShotForce = 20f;
    [SerializeField] private float maxArrowLength = 1.5f;

    public float MinShotForce => minShotForce;
    public float MaxShotForce => maxShotForce;

    public event Action OnShot;
    public event Action OnEnterHole;
    public event Action OnReset;

    private float moveTimer = 0f;
    [SerializeField] private float minMoveTime = 0.1f;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();
        lastShotPos = transform.position;
        aimArrow = GetComponent<LineRenderer>();
        CurrentState = BallState.IDLE;
    }

    private void Update()
    {
        followCameraTarget.transform.position = transform.position;
        yawOnly = Quaternion.Euler(0f, followCameraTarget.eulerAngles.y, 0f);
        CameraFoward = yawOnly * Vector3.forward;

        if (CurrentState == BallState.MOVING)
        {
            moveTimer += Time.deltaTime;

            if (Rb.linearVelocity.magnitude < 0.1f && moveTimer > minMoveTime)
            {
                Rb.linearVelocity = Vector3.zero;
                CurrentState = BallState.IDLE;
                moveTimer = 0f;
                Debug.Log("Ball idle");
            }
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
        OnShot?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole"))
        {
            Debug.Log("Ball in hole");
            CurrentState = BallState.INHOLE;
            OnEnterHole?.Invoke();
        }

        if (other.CompareTag("NearHole"))
        {
            //Suck the ball in
            Debug.Log("Sucking ball");
        }

        if (other.CompareTag("Bounds"))
        {
            Debug.Log("Out of bounds resetting to last shot!");
            transform.position = lastShotPos;
            Rb.linearVelocity = Vector3.zero;
            Rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            CurrentState = BallState.IDLE;
            OnReset?.Invoke();
        }
    }

}
