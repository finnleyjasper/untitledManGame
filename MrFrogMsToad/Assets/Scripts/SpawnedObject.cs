using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : PointObject
{
    public float activeTime;

    protected override void Eat(Player player)
    {
        GameMngr gm = FindObjectOfType<GameMngr>();
        gm.PointEaten(this, player);

    }
}
