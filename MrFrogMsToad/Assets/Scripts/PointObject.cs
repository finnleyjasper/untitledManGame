/* 
* filename: Fly.cs
* author: Finnley
* description: behaviour for the fly obj. comminicates with gamemngr to add scores to the players.
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 05 May 2024
* last modified:  09 May 2024
*/

/* Notes:
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class PointObject : EdibleObejct
{
    public int value = 10;
    public float respawnTime = 10f;

    protected override void Eat(Player player) 
    {
        FindObjectOfType<GameMngr>().PointEaten(this, player);

        if (!gameObject.active) // GameMngr will make a random fly active if none are, so ensures the method is only invoked if fly !active
        {
            Invoke("Respawn", respawnTime); // fly will reappear after 10 seconds
        } 
    }
}
