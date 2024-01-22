using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

        //flip player
        if (HorizontalInput > 0.01f)
            transform.localScale = new Vector3(4, 4, 4);
        else if(HorizontalInput < -0.01f)
            transform.localScale = new Vector3(-4, 4, 4);


        if (Input.GetKey(KeyCode.W) && grounded)
            Jump();

        //Set animation parameters
        animator.SetBool("run", HorizontalInput != 0);
        animator.SetBool("grounded", grounded);
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        animator.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
