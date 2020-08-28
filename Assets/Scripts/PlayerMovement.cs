using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public Animator animator;

    public float speed = 2;
    public float jumpSpeed = 2;

    private float xSpeed = 0;
    private bool isGrounded = true;

    private SpriteRenderer spriteRenderer;

    private AudioSource jumpSFX;
    private AudioSource footStepSFX;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        jumpSFX = GameObject.Find("Jump1").GetComponent<AudioSource>();
        footStepSFX = GameObject.Find("Footstep").GetComponent<AudioSource>();
    }

    void Update()
    {
        xSpeed = Input.GetAxis("Horizontal") * speed;

        playerBody.velocity = new Vector2(xSpeed, playerBody.velocity.y);

        walkAnimation();
        playWalkSFX();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            footStepSFX.Stop();
            jumpSFX.Play();
            animator.SetTrigger("Jump");
            playerBody.AddForce(Vector2.up * jumpSpeed * 100);
            isGrounded = false;
        }
    }

    private void playWalkSFX()
    {
        if (xSpeed != 0f && !footStepSFX.isPlaying && isGrounded)
        {
            footStepSFX.Play();
        }else if (xSpeed == 0f)
        {
            footStepSFX.Stop();
        }
    }

    private void walkAnimation()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else if (Input.GetAxis("Horizontal") == -1)
        {
            animator.SetBool("isWalking", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("isWalking", true);
            spriteRenderer.flipX = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GroundCollision"))
        {
            isGrounded = true;
        }
    }
}
