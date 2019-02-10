using UnityEngine;


/// <summary>
/// This Class controls the AIPlayer Movement
/// </summary>
public class AIPlayer : MonoBehaviour {


    public BallBehaviour Ball;

    public float speed = 30;

    // variable to smoothen the lerp 
    public float lerpTweak = 2f;

    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate to move rigidbodies
    void FixedUpdate()
    {
        Vector2 dir = new Vector2(0, 0);

        // Check the position of the ball relative to the AIPlayer
        // Using normalized to get only the direction .. Magnitude = 1
        if (Ball.transform.position.y > transform.position.y)
            dir = new Vector2(0, 1).normalized;

        else if (Ball.transform.position.y < transform.position.y)
            dir = new Vector2(0, -1).normalized;

        rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, dir * speed, lerpTweak * Time.deltaTime);

    }
}
