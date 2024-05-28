/* 
* filename: PlayerMovement.cs
* author: Finnley
* description: takes a collision from a player and changes their transform position to that of its "connection"
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 26 May 2024
* last modified:  27 May 2024
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform connection;
    private bool _active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players") && _active)
        {
            Vector3 position = collision.transform.position;
            position.x = connection.position.x;
            position.y = connection.position.y;
            collision.transform.position = position;
            _active = false;
            Invoke("Reactivate", 1.2f); // allows a slight delay so players so instantly re-teleport themselves back and forth
        }
    }

    private void Reactivate()
    {
        _active = true;
    }
}
