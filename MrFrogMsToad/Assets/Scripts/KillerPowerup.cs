/* 
* filename: KillerPowerup.cs
* author: Finnley
* description: inherits from EdibleObject. implements IPowerup. allows Player to "kill" the other player 
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 09 May 2024
* last modified:  22 May 2024
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerPowerup : SpawnedObject, IPowerup
{
    [SerializeField] private float _duration;
    private bool _actioned = false;

    protected override void Eat(Player player)
    {
        player.currentPowerup = this;
        FindObjectOfType<GameMngr>().PowerupEaten(this, player);
        gameObject.SetActive(false);
    }

    public void DoPowerup(Player player)
    {
        player.movement.speedMultiplier = 1.5f;
        player.CanKill = true;
        player.spriteRenderer.sprite = player.powerUpSprite;
        _actioned = true;
    }

    public void RevertPowerup(Player player)
    {
        player.movement.speedMultiplier = 1f;
        player.CanKill = false;
        player.spriteRenderer.sprite = player.normalSprite;
    }

    public float Duration
    {
        get { return _duration; }
    }

    public bool Actioned
    {
        get { return _actioned;  }
        set { _actioned = value; }
    }
}
