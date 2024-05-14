/* 
* filename: KillerPowerup.cs
* author: Finnley
* description: inherits from EdibleObject. implements IPowerup. allows Player to "kill" the other player 
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 09 May 2024
* last modified:  09 May 2024
*/

/* Notes:
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerPowerup : EdibleObejct, IPowerup
{
    [SerializeField] private float duration;

    protected override void Eat(Player player)
    {
        player.currentPowerup = "KillerPowerup";
        // player.movement.speed = (player.movement.speed * 150) / 100; -- need to reconfigure how player/powerup interacts so i can move
        // powerup stuff out of player

        FindObjectOfType<GameMngr>().PowerupEaten(this, player);
    }

    public float Duration
    {
        get { return duration; }
    }
}
