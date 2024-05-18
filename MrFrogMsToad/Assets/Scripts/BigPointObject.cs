using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPointObject : PointObject
{
    private void Awake()
    {
        respawnTime = 10; 
        value = 10;
    }

    protected override void Eat(Player player)
    {
        GameMngr gm = FindObjectOfType<GameMngr>();
        gm.PointEaten(this, player);
    }

}
