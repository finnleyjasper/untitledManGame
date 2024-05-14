using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKill : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            collision.gameObject.SetActive(false);

            Debug.Log(collision.gameObject.name + " died"); // DEBUGGING
        }
    }
}