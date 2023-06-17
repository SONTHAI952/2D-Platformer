using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private LayerMask jumpableGround;

    private float directionX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private AudioSource jumpAudio;

    private enum MovementState { idle, run, jump, fall }
    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(directionX * moveSpeed, rigidbody2D.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpAudio.Play();
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        MovementState state;
        if (directionX > 0f)
        {
            state = MovementState.run;
            spriteRenderer.flipX = false;
        }
        else if (directionX < 0f)
        {
            state = MovementState.run;
            spriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rigidbody2D.velocity.y > 0.01f)
        {
            state = MovementState.jump;
        }
        else if(rigidbody2D.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }
        animator.SetInteger("State",(int) state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
