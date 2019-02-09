using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 30.0f;

    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float vert = Input.GetAxisRaw("Vertical");
        rigidbody2d.velocity = new Vector2(0, vert * speed);
    }
}
