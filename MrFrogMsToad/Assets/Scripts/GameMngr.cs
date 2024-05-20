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
    private float _bigPointObjectsTimeLastActive = 3f;

    public GameObject gameOverScreen;

    private void Start()
    {
        NewGame();
    }

    private void FixedUpdate()
    {
        if (_gameState == GameState.playing)
        {
            if (bigPointObjects.childCount > 0)
            {
                MangeSpawnedObject(bigPointObjects, _bigPointObjectsTimeLastActive);
            }  
        }

        if (_gameState == GameState.gameOver && Input.GetKeyUp(KeyCode.Space))
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        gameOverScreen.SetActive(false);
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

    // TELEPORTERS
    public void MovePlayer(Player player, Vector2 endPosition)
    {

    }

    // POWERUP MANAGEMENT

    public void PowerupEaten(IPowerup powerup, Player player) 
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

    // SPAWNED OBJ MANAGEMNT

    private void TurnOffAllBigPoints()
    {
        foreach (Transform bpo in this.bigPointObjects)
        {
            bpo.gameObject.SetActive(false);
        }
    }

    private void MangeSpawnedObject(Transform spawnedObjects, float timeLastActive)
    {
        bool active = false;

        foreach (Transform spObject in spawnedObjects)
        {
            if (spObject.gameObject.active)
            {
                active = true;
            }
        }

        Transform aSpawnedObjectChild = spawnedObjects.GetChild(0);
        SpawnedObject aSpawnedObject = aSpawnedObjectChild.GetComponent<SpawnedObject>();

        if (!active && Time.time > timeLastActive + aSpawnedObject.respawnTime + aSpawnedObject.activeTime)
        {
            float randomNumber = Random.Range(1, spawnedObjects.childCount);

            foreach (Transform spObject in spawnedObjects)
            {
                if (spObject.GetSiblingIndex() == randomNumber)
                {
                    SpawnedObject chosenSpawnedObject = spObject.GetComponent<SpawnedObject>();
                    chosenSpawnedObject.Respawn();
                    DelayedPointDespawn(chosenSpawnedObject.activeTime, chosenSpawnedObject);
                    timeLastActive = Time.time + chosenSpawnedObject.activeTime;                 
                }
            }
        }
    }

    // DELAY POINT/POWERUP RESPAWN
    void DelayedPointDespawn(float delayTime, SpawnedObject spawnedObject)
    {
        StartCoroutine(DelayPointDespawn(delayTime, spawnedObject));
    }

    IEnumerator DelayPointDespawn(float delayTime, SpawnedObject spawnedObject)
    {
        yield return new WaitForSeconds(delayTime);
        spawnedObject.gameObject.SetActive(false);
    }

    // DELAY PLAYER RESPAWN
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

    
