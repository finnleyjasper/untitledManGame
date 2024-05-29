using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{public Player[] players; 
public AudioClip powerupclip ; 
public AudioClip gameclip ;
private AudioClip currentclip ;
private AudioSource audioSource ;
private void Start(){
audioSource = GetComponent<AudioSource>() ;
}
private void Update()
    { bool poweredup = false;
    foreach (Player player in players)
    {
            if (player.currentPowerup != null)
            {
                poweredup = true;
            }
    
        }    
       

        if (poweredup == false)
        {
            currentclip = gameclip;
        }
        else 
        {
            currentclip = powerupclip;
            
        } Debug.Log("CurrentclipUwU" + currentclip);
        audioSource.clip = currentclip ;
        audioSource.Play();
    }
}

