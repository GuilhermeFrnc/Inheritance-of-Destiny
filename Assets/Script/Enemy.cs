using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool ground = true;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool faceRight = true;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);

        if (!ground)
        {
            speed *= -1;
        }

        if (speed > 0 && !faceRight)
        {
            Flip();
        }
        else if (speed < 0 && faceRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        faceRight = !faceRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            speed = 0;
            Animator enemyAnimator = GetComponent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetBool("Attack", true);
            }

            StartCoroutine(PlayerDeathSequence(collision.gameObject));
        }
    }

    private IEnumerator PlayerDeathSequence(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);

        Animator enemyAnimator = GetComponent<Animator>();
        if (enemyAnimator != null)
        {
            enemyAnimator.SetBool("Attack", false);
        }

        Animator playerAnimator = player.GetComponent<Animator>();
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        CapsuleCollider2D playerCollider = player.GetComponent<CapsuleCollider2D>();
        Player playerScript = player.GetComponent<Player>();

        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Dead");
            playerAnimator.SetBool("Jump", false);
        }

        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        if (playerScript != null)
        {
            playerScript.enabled = false;
        }

        Invoke("LoadScene", 1f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void Die()
    {
        // Desativa os colliders
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        // Obtém uma referência ao Rigidbody do inimigo
        Rigidbody2D enemyRigidbody = GetComponent<Rigidbody2D>();
        if (enemyRigidbody != null)
        {
            // Define o Rigidbody como Kinematic para evitar que o inimigo caia
            enemyRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        // Executa a animação de morte
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }

        // Deixa o inimigo inativo após um tempo
        StartCoroutine(DisableEnemy());
    }

    private IEnumerator DisableEnemy()
    {
        yield return new WaitForSeconds(1f); // Ajuste o tempo conforme necessário
        gameObject.SetActive(false);
    }
}
