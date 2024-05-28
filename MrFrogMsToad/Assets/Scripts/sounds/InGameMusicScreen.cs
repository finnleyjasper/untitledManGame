using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{public Player[] players; 
public AudioClip powerupclip ; 
public AudioClip gameclip ;
private void Update(){
    foreach (Player player in players)
    {
        if (player.currentPowerup != null)
        {
            
        }
    }
}
}
