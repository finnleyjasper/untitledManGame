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
            Invoke("Reactivate", 2f);
        }
    }

    private void Reactivate()
    {
        _active = true;
    }
}
