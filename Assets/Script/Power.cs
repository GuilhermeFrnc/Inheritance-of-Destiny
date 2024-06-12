using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public float lifetime = 2f;
    private AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.Play();
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }
        }
        Destroy(gameObject);
    }
}
