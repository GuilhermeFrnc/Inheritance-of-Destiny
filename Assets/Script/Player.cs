using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer sr;
    public float jumpForge;
    public bool inFloor = true;
    void Start()
    {
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
    }
    void MovePlayer()
    {
        float horizontalMoviment = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalMoviment * Time.deltaTime * speed, 0, 0);
        if (horizontalMoviment > 0)
        {
            sr.flipX = false;
        }

        if (horizontalMoviment < 0)
        {
            sr.flipX = true;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && inFloor)
        {
            rbPlayer.AddForce(new Vector2(0, jumpForge), ForceMode2D.Impulse);
            inFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            inFloor = true;
        }
    }
}
