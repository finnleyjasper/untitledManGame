using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Player[] players;

    public AudioClip powerupClip; 
    public AudioClip gameClip;
    public AudioClip gameOverClip;

    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    private void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PlayerHasPowerUp() && GameIsInPlayState())
        {
            musicAudioSource.clip = powerupClip;
        }
        else if(!GameIsInPlayState())
        {
            musicAudioSource.clip = gameOverClip;
        }
        else 
        {
            musicAudioSource.clip = gameClip;   
        }

        PlayMusic();
    }

    private bool GameIsInPlayState()
    {
        GameMngr gm = FindObjectOfType<GameMngr>();
        bool isPlaying = false;

        if(gm.gameState == GameMngr.GameState.playing)
        {
            isPlaying = true;
        }

        return isPlaying;
    }

    private bool PlayerHasPowerUp()
    {
        bool poweredup = false;

        foreach (Player player in players)
        {
            if (player.currentPowerup != null)
            {
                poweredup = true;
            }
        }

        return poweredup;
    }

    private void PlayMusic()
    {
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxAudioSource.clip = audioClip;
        sfxAudioSource.PlayOneShot(audioClip);
    }
}

