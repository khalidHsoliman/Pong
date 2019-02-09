using UnityEngine;

public class AIPlayer : MonoBehaviour {


    public BallBehaviour Ball;

    public float speed = 30;
    public float lerpTweak = 2f;

    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 dir = new Vector2(0, 0);

        if (Ball.transform.position.y > transform.position.y)
            dir = new Vector2(0, 1).normalized;

        else if (Ball.transform.position.y < transform.position.y)
            dir = new Vector2(0, -1).normalized;

        rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, dir * speed, lerpTweak * Time.deltaTime);

    }
}
