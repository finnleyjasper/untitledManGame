/*
* filename: GameManagerScript.cs
* author: Finnley
* description: GameManager manages invisable aspects of the game such as points, time, lives, etc.
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*              - https://forum.unity.com/threads/what-is-the-best-way-to-delay-a-function.1002040/ (coroutine code)
*
* created: 02 May 2024
* last modified:  09 May 2024
*/

/* Notes:
 *  02/05: Must remember to turn off all powerups and fruits too in GameOver
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMngr : MonoBehaviour
{
    [SerializeField] int eatenBonus;

    private enum GameState {playing, gameOver};
    private GameState _gameState;

    public Player[] players;
    public Transform pointObjects;
    public GameObject gameOverScreen;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (_gameState == GameState.gameOver)
        {
            if (Input.anyKeyDown)
            {
                NewGame();
            }
        }
    }

    private void NewGame()
    {
        gameOverScreen.SetActive(false);
        _gameState = GameState.playing;

        foreach (Transform point in this.pointObjects)
        {
            point.gameObject.SetActive(true); // putting all the flies back in the map
        }

        foreach (Player player in this.players)
        {
            player.Reset(); 
        }
    }

    private void GameOver() // don't want to reset scores/lives as you want to show a 'game over' screen w/ results
    {
        // turns off all flies and players
        foreach (Transform basicEdible in this.pointObjects)
        {
            basicEdible.gameObject.SetActive(false);
        }

        foreach (Player player in this.players)
        {
            player.gameObject.SetActive(false);
        }

        _gameState = GameState.gameOver;
        gameOverScreen.SetActive(true);
    }

    public void PlayerEaten(Player winner, Player loser) 
    {
        // winner adds the bonus to their score, loser has 1 life removed
        winner.IncreaseScore(eatenBonus);
        loser.LoseLife();

        if (loser.Lives > 0)
        {
            DelayedRespawn(3.0f, loser);          
        }
        else
        {
            Invoke(nameof(GameOver), 0.25f);
        }
    }

    public void PointEaten(PointObject point, Player player)
    {
        point.gameObject.SetActive(false);

        player.IncreaseScore(point.value);
        Debug.Log(player.Score); // REMOVE -- DEBUGGING
    }

    // POWERUP MANAGEMENT

    public void PowerupEaten(IPowerup powerup, Player player) //doesnt need player but might add functionality later?
    {
        powerup.DoPowerup(player);
        RemovePowerup(player);
    }

    public void RemovePowerup(Player player) // begins the count down to remove the powerup
    {
        StartCoroutine(PowerupCountDown(player.currentPowerup.Duration, player));
    }

    IEnumerator PowerupCountDown(float delayTime, Player player) // revert changes from the powerup here
    {
        yield return new WaitForSeconds(delayTime);
        player.RemovePowerup();
    }


    // vv methods for future to ensure all flies are on -- want to work on after tilemap/fly tiles are made

    /* private void TurnOnMinimumFlies()
    {
        Debug.Log(CheckFlies());
        if(!CheckFlies())
        {
            float flyToActivatef = Random.Range(0f, flies.Length);
            int flyToActivatei = (int)flyToActivatef;
            flies[flyToActivatei].Respawn();
        }
    }

    private bool CheckFlies()
    {
        bool active = true;

        foreach (Fly fly in flies)
        {
            if(!fly.gameObject.active)
            {
                active = false;
            }
        }
        return active;
    }*/


    // DELAY RESPAWN
    void DelayedRespawn(float delayTime, Player player)
    {
        StartCoroutine(DelayAction(delayTime, player));
    }

    IEnumerator DelayAction(float delayTime, Player player)
    {
        //wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        player.Respawn();
        //do the action after the delay time has finished.
    }
}

    
