using System.Collections;
using UnityEngine;

/// <summary>
/// This class handles all the collisions of the ball
/// </summary>
public class BallBehaviour : MonoBehaviour {

    public float speed = 40.0f;

    private float oldSpeed = 0.0f; 

    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        // initially pushing the ball to the right
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = Vector2.right * speed; 
    }

    private void Update()
    {
        if (GameManager.gm)
        {
            // stops the ball if gameIsover
            if (GameManager.gm.gameIsOver)
                rigidbody2d.velocity = Vector2.zero;
        }
    }


    /// <summary>
    /// Checks the collisions with playre, Ai, upper and lower wall, right and left goals
    /// using tags instead of names is more consistent and best practice 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AI"))
        {
            PaddleCollide(collision);

            SoundManager.SM.PlayOneShot(SoundManager.SM.hitPaddleBloop);
        }


        else if (collision.gameObject.CompareTag("RightGoal") || collision.gameObject.CompareTag("LeftGoal"))
        {
            SoundManager.SM.PlayOneShot(SoundManager.SM.goalBloop);

            GameManager.gm.IncreaseScore(collision.gameObject);

            StartCoroutine("Wait");
        }

        else if (collision.gameObject.CompareTag("UpperWall") || collision.gameObject.CompareTag("BottomWall"))
        {
            SoundManager.SM.PlayOneShot(SoundManager.SM.wallBloop);
        }
    }

    /// <summary>
    /// a fuction to handle the ball reaction when colliding with the player or ai paddle
    /// </summary>
    /// <param name="collision"></param>
    private void PaddleCollide(Collision2D collision)
    {
        // ratio is a variable that controls the dir where the ball will bounce to
        // if the ball hits above the paddle center the dir will angles up , ratio pos
        // if the ball hits below the paddle center the dir will angles down , ratio neg
        float ratio = (transform.position.y - collision.transform.position.y) 
                        / collision.collider.bounds.size.y;

        //Debug.Log(ratio);

        Vector2 dir = new Vector2();

        // using normalized to get only the dir 
        if (collision.gameObject.CompareTag("Player"))
            dir = new Vector2(-1, ratio).normalized;

        else if (collision.gameObject.CompareTag("AI"))
            dir = new Vector2(1, ratio).normalized;

        rigidbody2d.velocity = dir * speed; 
    }


    /// <summary>
    /// Checks when the ball triggers pickups' colliders 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lightning"))
            GameManager.gm.Speedup();

        else if (collision.CompareTag("Magnet"))
            GameManager.gm.ChangeDir();

        // hide the pickups after collecting them
        collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }


    /// <summary>
    /// This coroutine make the game stops for a little time when scoring a goal
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        oldSpeed = speed; 
        speed = 0.0f;
        rigidbody2d.velocity = Vector2.zero; 

        yield return new WaitForSeconds(0.5f);
        gameObject.transform.position = new Vector2(0, 0);

        yield return new WaitForSeconds(0.5f);
        speed = oldSpeed;
        rigidbody2d.velocity = Vector2.right * speed;
    }
}
