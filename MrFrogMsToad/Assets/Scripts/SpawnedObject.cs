/* 
* filename: SpawnedObject.cs
* author: Finnley
* description: inherits from PointObject to remove the instant respawn from Eat() and include an "activeTime"
*
* created: 15 May 2024
* last modified:  15 May 2024
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : PointObject
{
    public float activeTime; // time the spawned object stays spawned

    protected override void Eat(Player player)
    {
        GameMngr gm = FindObjectOfType<GameMngr>();
        gm.PointEaten(this, player);

    }
}
