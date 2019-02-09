﻿using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    public float speed = 30.0f;

    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = Vector2.right * speed; 
    }

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

            // freeze and count score 

            gameObject.transform.position = new Vector2(0, 0);
            rigidbody2d.velocity = Vector2.right * speed;

        }

        else if (collision.gameObject.CompareTag("UpperWall") || collision.gameObject.CompareTag("BottomWall"))
        {
            SoundManager.SM.PlayOneShot(SoundManager.SM.wallBloop);
        }
    }

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
}