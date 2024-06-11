using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    internal static object current;
    public Transform player;

    private void FixedUpdate()
    {
        Vector3 newPosition = player.position + new Vector3(0, 0, -10);
        newPosition.y = 0.1f;
        transform.position = newPosition;
    }
}
