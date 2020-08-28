using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 0.3f;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private ParticleSystem particles;
    private AudioSource enemyDeadSFX;

    private float timeBeforeChange;
    private float delay = 2f;
    private bool isPlayerAbove = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        particles = GameObject.Find("EnemyParticles").GetComponent<ParticleSystem>();
        enemyDeadSFX = GameObject.Find("EnemyDead").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = Vector2.right * speed;

        if (timeBeforeChange < Time.time)
        {
            speed *= -1;

            walkAnimation();

            timeBeforeChange = Time.time + delay;
        }
    }

    private void walkAnimation()
    {
        if (speed < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerAbove = transform.position.y + 0.3f < collision.transform.position.y;

            if (isPlayerAbove)
            {
                enemyDeadSFX.Play();
                particles.transform.position = transform.position;
                particles.Play();
                animator.SetBool("isDead", true);
            }
        }
    }

    public void disableEnemy()
    {
        Destroy(gameObject);
    }
}
