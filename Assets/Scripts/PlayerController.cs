using UnityEngine;

/// <summary>
/// This class controls player movement and behaviour 
/// </summary>
public class PlayerController : MonoBehaviour {

    public float speed = 40.0f;

    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // check for input (w or s) keys .. w = 1 and s = -1
        float vert = Input.GetAxisRaw("Vertical");
        rigidbody2d.velocity = new Vector2(0, vert * speed);
    }
}
