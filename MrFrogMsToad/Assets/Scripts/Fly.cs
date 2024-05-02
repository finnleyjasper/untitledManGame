using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{

    public int points = 10;

    private void Eat()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.layer == LayerMask.NameToLayer("Players"))
       {
            Eat();
       }
    }
}
