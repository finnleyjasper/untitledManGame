/*filename: IPowerup.cs
* author: Finnley
* description: an interface for powerups; all powerups must have a duration
*
*created: 09 May 2024
* last modified:  12 May 2024
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerup
{
    [SerializeField] public float Duration { get; } // how long the powerup effect lasts

    public bool Actioned { get; set; }

    public void DoPowerup(Player player);

    public void RevertPowerup(Player player);

}
