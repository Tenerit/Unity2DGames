﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public bool isJumping = false;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;


    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;



    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
        MovePlayer(horizontalMovement);


        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        MovePlayer(horizontalMovement);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

    }



    void MovePlayer(float _horizontalMoement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMoement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip(float _velocity) { 
        if (_velocity> 0.1f)
        {
            spriteRenderer.flipX = false;

        }else if(_velocity < -0.1f){
            spriteRenderer.flipX = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); 
    }
}
