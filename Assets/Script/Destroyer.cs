using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroyer : MonoBehaviour
{
    private AudioSource deathAudioSourcePlayer;
    public AudioClip deathSoundPlayer;

    private void Start()
    {
        deathAudioSourcePlayer = GetComponent<AudioSource>();
        if (deathAudioSourcePlayer == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            deathAudioSourcePlayer.clip = deathSoundPlayer;
            deathAudioSourcePlayer.Play();
            StartCoroutine(WaitForDeathSound(deathAudioSourcePlayer.clip.length));
        }
    }

    private IEnumerator WaitForDeathSound(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("GameOver");
    }
}
