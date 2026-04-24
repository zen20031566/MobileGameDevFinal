using UnityEngine;

public enum BallState
{
    SPAWNING, IDLE, MOVING, INHOLE
}

public class Ball : MonoBehaviour
{
    public Rigidbody Rb {  get; private set; }
    public Collider Col {  get; private set; }  

    public BallState CurrentState {  get; private set; }

    private Vector3 lastShotPos;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();
        CurrentState = BallState.SPAWNING;
        lastShotPos = transform.position;
    }

}
