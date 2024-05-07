/* 
* filename: PlayerKill.cs
* author: Finnley
* description: reusable script for anything that can be killed by a player
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 02 May 2024
* last modified:  07 May 2024
*/

/* Notes:
 * 
 */

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
