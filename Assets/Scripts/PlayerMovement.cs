using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;

    private Rigidbody2D rigidBody;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private bool isGrounded;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Procura por um RigidBody2D no game object a que está ligado. Neste caso, o Player.
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        GetInputs();

        // Animate
        Animate();

        

        // Move
        Move();


    }

    private void FixedUpdate()
    {
        // Check if grounded. Only jumps after touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
    }

    private void GetInputs()
    {
        moveDirection = Input.GetAxis("Horizontal"); // Scale of -1 <> 1. "Horizontal" esta' hardcoded no unity, como "Jump"
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void Move()
    {
        rigidBody.velocity = new Vector2(moveDirection * moveSpeed, rigidBody.velocity.y);
        if(isJumping)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
        }
        isJumping = false;
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
