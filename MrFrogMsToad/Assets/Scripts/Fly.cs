using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Fly : MonoBehaviour
{
    public int points = 10;

    private void Eat() //should take a player
    {
        FindObjectOfType<GameMngr>().FlyEaten(this); //pass the player
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
                // Player player = other.gameObject;
                Eat(); // should pass player
        } 
    }
}
