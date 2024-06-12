using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer sr;
    public float jumpForge;
    public bool inFloor = true;
    public bool doubleJump;
    public Transform powerSpawnPointLeft;
    public Transform powerSpawnPointRight;
    public GameObject powerPrefab;
    public float powerSpeed;
    private AudioSource sound;
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    void Update()
    {
        Jump();
        Power();
    }

    void Power()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("Player_power");
            Transform selectedSpawnPoint = sr.flipX ? powerSpawnPointLeft : powerSpawnPointRight;
            var power = Instantiate(powerPrefab, selectedSpawnPoint.position, selectedSpawnPoint.rotation);
            Vector2 direction = sr.flipX ? -selectedSpawnPoint.right : selectedSpawnPoint.right;
            power.GetComponent<Rigidbody2D>().velocity = direction * powerSpeed;

            Collider2D powerCollider = power.GetComponent<Collider2D>();
            Collider2D characterCollider = GetComponent<Collider2D>();
            if (powerCollider != null && characterCollider != null)
            {
                Physics2D.IgnoreCollision(characterCollider, powerCollider, true);
                StartCoroutine(ReenableCollision(characterCollider, powerCollider));
            }
        }
    }

    private IEnumerator ReenableCollision(Collider2D characterCollider, Collider2D powerCollider)
    {
        yield return new WaitForSeconds(0.1f);
        if (characterCollider != null && powerCollider != null)
        {
            Physics2D.IgnoreCollision(characterCollider, powerCollider, false);
        }
    }
    void MovePlayer()
    {
        float horizontalMoviment = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalMoviment * Time.deltaTime * speed, 0, 0);
        if (horizontalMoviment > 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = false;
        }

        else if (horizontalMoviment < 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = true;
        }
        else
        {
            playerAnim.SetBool("Walk", false);
        }
    }

    void Jump()
    {
        sound = GetComponent<AudioSource>();
        if (Input.GetButtonDown("Jump"))
        {
            if (inFloor)
            {
                playerAnim.SetBool("Jump", true);
                sound.Play();
                rbPlayer.AddForce(new Vector2(0, jumpForge), ForceMode2D.Impulse);
                inFloor = false;
                doubleJump = true;
            }
            else if (inFloor == false && doubleJump)
            {
                playerAnim.SetBool("Jump", true);
                sound.Play();
                rbPlayer.AddForce(new Vector2(0, jumpForge), ForceMode2D.Impulse);
                inFloor = false;
                doubleJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            playerAnim.SetBool("Jump", false);
            inFloor = true;
            doubleJump = false;
        }
    }
}
