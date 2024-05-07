/* 
* filename: Fly.cs
* author: Finnley
* description: behaviour for the fly obj. comminicates with gamemngr to add scores to the players.
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 05 May 2024
* last modified:  07 May 2024
*/

/* Notes:
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Fly : MonoBehaviour
{
    public int points = 10;

    private void Eat(GameObject player) 
    {
        FindObjectOfType<GameMngr>().FlyEaten(this, player); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Players"))
        {   
                Eat(other.gameObject); 
        } 
    }
}
