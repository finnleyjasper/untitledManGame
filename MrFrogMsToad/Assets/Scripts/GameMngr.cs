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

    public enum GameState {playing, gameOver};
    public GameState _gameState;

    public Player[] players;
    public Transform pointObjects;
    public Transform bigPointObjects;
    public GameObject gameOverScreen;

    private float _howOftenToSpawnBigPoints;
    private float _lastTimeBigPointSpawned;

    private void Awake()
    {
        int count = bigPointObjects.GetChildCount();
        Transform bigPointTrans = bigPointObjects.GetChild(count-1);
        Debug.Log(bigPointTrans.gameObject.name);
        BigPointObject bigPoint = bigPointTrans.gameObject.GetComponent<BigPointObject>();
        _howOftenToSpawnBigPoints = bigPoint.respawnTime;
        _lastTimeBigPointSpawned = Time.fixedTime + _howOftenToSpawnBigPoints;
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (_gameState == GameState.playing)
        {
            MangeBigPoints();
        }

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
        // gameOverScreen.SetActive(false);
        _gameState = GameState.playing;

        TurnOffAllBigPoints();

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
        foreach (Transform points in this.pointObjects)
        {
            points.gameObject.SetActive(false);
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

    // BIG PONT MANAGEMNT

    private void TurnOffAllBigPoints()
    {
        foreach (Transform bpo in this.bigPointObjects)
        {
            bpo.gameObject.SetActive(false);
        }
    }

    private void MangeBigPoints()
    {
        bool mustSpawn = false;
        bool oneActive = false;

        foreach (Transform bigPoint in bigPointObjects)
        {
            // if one point is already active, OR none are active but not enough time has passed to spawn another
            if (bigPoint.gameObject.active)
            {
                oneActive = true;
            }
        }

        if (!oneActive && _lastTimeBigPointSpawned > (Time.fixedTime - _howOftenToSpawnBigPoints))
        {
            mustSpawn = true;
        }

        if (mustSpawn)
        {
            float randomNumber = Random.Range(1, bigPointObjects.childCount);

            foreach (Transform bigPoint in bigPointObjects)
            {
                if (bigPoint.GetSiblingIndex() == randomNumber)
                {
                    bigPoint.gameObject.SetActive(true);
                    _lastTimeBigPointSpawned = Time.fixedDeltaTime;
                }
            }
        }
    }

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

    
