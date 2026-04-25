using UnityEngine;

public class NearHole : MonoBehaviour
{
    [SerializeField] float suckingForce = 10f;
    private BoxCollider col;
    private Ball ball;
    private bool isSucking;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ball = other.GetComponent<Ball>();
            isSucking = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isSucking = false;
            ball = null;
        }
    }

    void FixedUpdate()
    {
        if (!isSucking || ball == null || ball.CurrentState == BallState.INHOLE) return;

        Vector3 direction = transform.position - ball.transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;
        float maxDistance = col.size.x * 0.5f;

        float strength = Mathf.InverseLerp(maxDistance, 0f, distance) * suckingForce;

        ball.Rb.AddForce(direction.normalized * strength);
    }
}