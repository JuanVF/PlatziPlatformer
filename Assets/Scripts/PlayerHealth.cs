using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public Image[] hearths;
    public SceneChanger sceneChanger;

    private bool hasCooldown = false;
    private bool enemyCollision = false;
    private bool isPlayerAbove = false;

    private AudioSource damageSFX;

    private void Start()
    {
        damageSFX = GameObject.Find("Damage").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyCollision = collision.gameObject.CompareTag("Enemy");
        isPlayerAbove = transform.position.y + 2f < collision.transform.position.y - 2f;


        if (enemyCollision && !isPlayerAbove)
        {
            substractHealth();
        }
    }

    private void substractHealth()
    {
        EmptyHearths();

        if (!hasCooldown && health > 0)
        {
            health--;
            hasCooldown = true;

            StartCoroutine(Cooldown());
        }

        if (!hasCooldown && health <= 0)
        {
            sceneChanger.changeSceneTo("LoseScene");
        }

    }

    private void EmptyHearths()
    {
        if (health > 0 && !hasCooldown)
        {
            damageSFX.Play();
            hearths[health - 1].gameObject.SetActive(false);
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2f);

        hasCooldown = false;

        StopCoroutine(Cooldown());
    }
}
