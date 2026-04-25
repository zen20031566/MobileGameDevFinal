using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball Ball;
    //Hole hole
    //Start pos

    private Transform startPoint;
    private Transform hole;

    private void Awake()
    {
        //startPoint = GameObject.FindWithTag("YourTag").transform;
        //if (startPoint == null) Debug.LogError(SceneManager.GetActiveScene().name + " start point cannot be found");

        //hole = GameObject.FindWithTag("Hole").transform;
        //if (startPoint == null) Debug.LogError(SceneManager.GetActiveScene().name + " hole cannot be found");

        TouchController.Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
