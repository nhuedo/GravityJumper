using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float speed = 3f;
    public float jumpSpeed = 10f;
    private float direction = 0;

    private Rigidbody2D rb;

    private Animator playerAnimation;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    public SceneControllerScript scController;
    
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        direction = Input.gyro.rotationRateUnbiased.y;

        if (direction > 0f)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            transform.localScale = new Vector2(0.24f, 0.24f);
        }
        else if ( direction < -0.5f)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            transform.localScale = new Vector2(-0.24f, 0.24f);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && isTouchingGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            scController.points++;
            Destroy(collision.gameObject);
        }
    }
}
