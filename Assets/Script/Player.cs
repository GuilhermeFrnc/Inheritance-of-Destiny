using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }
    void Update()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        float horizontalMoviment = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalMoviment * Time.deltaTime * speed,0,0);
    }
}
