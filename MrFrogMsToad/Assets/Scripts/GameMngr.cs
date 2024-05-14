/*
* filename: GameManagerScript.cs
* author: Finnley
* description: GameManager manages invisable aspects of the game such as points, time, lives, etc.
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 02 May 2024
* last modified:  02 May 2024
*/

/* Notes:
 *  02/05: Must remember to turn off all powerups and fruits too in GameOver
 *  02/05: "ResetState" -- may need to include respawning player in spawn location?
 *  02/05: Double check the delay for death works correctly
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMngr : MonoBehaviour
{
    [SerializeField] int startingLives;
    [SerializeField] int eatenBonus;

    private enum GameState {playing, gameOver};
    private GameState _gameState;

    public Player[] players;
    public Transform flies;


    //public int score { get; private set; } // means we can manually see it, but not set it; only actions in-game able to set the score/lives
    //public int lives { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Input.anyKeyDown && _gameState == GameState.gameOver)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        _gameState = GameState.playing;

        foreach (Transform fly in this.flies)
        {
            fly.gameObject.SetActive(true); // putting all the flies back in the map
        }

        foreach (Player player in this.players)
        {
            ResetPlayerState(player);
            ResetPlayerStats(player);
        }
    }

    private void ResetPlayerState(Player player) // resets a single player
    {
        player.gameObject.SetActive(true);
    }

    private void ResetPlayerStats(Player player)
    {
        player.Score = 0;
        player.Lives = startingLives;
    }

    private void GameOver() // don't want to reset scores/lives as you want to show a 'game over' screen w/ results
    {
        // turns off all flies and players
        foreach (Transform fly in this.flies)
        {
            fly.gameObject.SetActive(false);
        }

        foreach (Player player in this.players)
        {
            player.gameObject.SetActive(false);
        }

        _gameState = GameState.gameOver;
    }

    public void PlayerEaten(Player winner, Player loser)
    {
        // winner adds the bonus to their score, loser has 1 life removed
        winner.Score += eatenBonus;

        loser.Lives -= 1;
        loser.gameObject.SetActive(false);

        if (loser.Lives > 0)
        {
            DoDelayAction(3.0f);
            ResetPlayerState(loser);
        }
        else
        {
            GameOver();
        }
    }

    public void FlyEaten(Fly fly) // should also take a player as a parameter, fetched from fly
    {
        fly.gameObject.SetActive(false);

        players[0].Score += fly.points; // temp hardcoded player ------- FIX!! -------
        Debug.Log(players[0].Score); // REMOVE -- DEBUGGING
    }

    // coroutine code: https://forum.unity.com/threads/what-is-the-best-way-to-delay-a-function.1002040/

    void DoDelayAction(float delayTime)
    {
        StartCoroutine(DelayAction(delayTime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        //wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //do the action after the delay time has finished.
    }
}

    
