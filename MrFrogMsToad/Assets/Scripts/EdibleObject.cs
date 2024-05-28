/* 
* filename: EdibleObject.cs
* author: Finnley
* description: a parent obj for powerups, flies, etc. - anything that should be able to get "eaten" by the players.
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 09 May 2024
* last modified: 19 May 2024
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleObejct : MonoBehaviour
{
    protected virtual void Eat(Player player)
    {
        // put what will happen when this obj is eaten here
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            Player player = other.GetComponent<Player>();
            Eat(player);
        }
    }
}
