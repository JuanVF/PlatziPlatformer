using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public Text collectableText;
    public static int collectableQuant;

    private ParticleSystem particles;
    private AudioSource collectableSFX;

    public void Start()
    {
        particles = GameObject.Find("CollectableParticle").GetComponent<ParticleSystem>();
        collectableSFX = GameObject.Find("Coin").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collectableSFX.Play();

            particles.Play();
            particles.transform.position = transform.position;

            collectableQuant++;

            setCollectableText();
            Destroy(gameObject);
        }
    }

    private void setCollectableText()
    {
        if (collectableQuant < 10)
        {
            collectableText.text = "x0" + collectableQuant.ToString();
        }
        else
        {
            collectableText.text = "x" + collectableQuant.ToString();
        }
    }
}
