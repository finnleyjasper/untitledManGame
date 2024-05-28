/*
* filename: GameManagerScript.cs
* author: Finnley
* description: GameManager manages invisable aspects of the game such as points, time, lives, game phase, screens to show etc.
* 
*              game controls such as the points needed to win or the bonus for eating another player is set here
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*              - https://forum.unity.com/threads/what-is-the-best-way-to-delay-a-function.1002040/ (coroutine code)
*
* created: 02 May 2024
* last modified:  28 May 2024
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMngr : MonoBehaviour
{
    [SerializeField] int eatenBonus;
    [SerializeField] int pointCapToWin;

    public enum GameState {playing, gameOver};
    public GameState _gameState;

    public Player[] players;
    public Transform pointObjects;

    public Transform bigPointObjects;
    private float _bigPointObjectsTimeLastActive = 0;

    public Transform killerPowerups;
    private float _killerPowerupTimeLastActive = 0;

    public GameObject gameOverScreen;
    public GameObject ui;

    private Player _winner;
    private Player _loser;
    private string _reasonForGameOver; 

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (_gameState == GameState.playing)
        {
            if (bigPointObjects.childCount > 0)
            {
                if (SpawnNewObject(bigPointObjects, _bigPointObjectsTimeLastActive))
                {
                    _bigPointObjectsTimeLastActive = Time.time;
                }
            }

            if (killerPowerups.childCount > 0)
            {
                if (SpawnNewObject(killerPowerups, _killerPowerupTimeLastActive))
                {
                    _killerPowerupTimeLastActive = Time.time;
                }
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
        _winner = null;
        _loser = null;
        _gameState = GameState.playing;
        _killerPowerupTimeLastActive = Time.time;
        _bigPointObjectsTimeLastActive = Time.time;

        TurnOffAllSpawnableObjects(bigPointObjects);
        TurnOffAllSpawnableObjects(killerPowerups);

        foreach (Transform point in this.pointObjects)
        {
            point.gameObject.SetActive(true); // putting all the flies back in the map
        }

        foreach (Player player in this.players)
        {
            player.Reset(); 
        }

        ui.SetActive(true); 
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
        ui.SetActive(false);
        _gameState = GameState.gameOver;
        gameOverScreen.SetActive(true);
        GameoverWinnerText gameOverUi = gameOverScreen.GetComponentInChildren<GameoverWinnerText>();
        gameOverUi.UpdateText(_winner, _loser, _reasonForGameOver);
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
            _winner = winner;
            _loser = loser;
            _reasonForGameOver = _winner.name + " has eaten " + _loser.name + " 3 times!";
            GameOver();
        }
    }

    public void PointEaten(PointObject point, Player player)
    {
        point.gameObject.SetActive(false);

        player.IncreaseScore(point.value);

        if (player.Score >= pointCapToWin)
        {
            _winner = player;

            foreach(Player p in players)
            {
                if (p != player)
                {
                    _loser = p;
                }
            }

            _reasonForGameOver = _winner.name + " has eaten 75 points before " + _loser.name + "!";
            GameOver();
        }
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

    private void TurnOffAllSpawnableObjects(Transform spawnableObjs)
    {
        foreach (Transform spo in spawnableObjs)
        {
            spo.gameObject.SetActive(false);
        }
    }

    private bool SpawnNewObject(Transform objects, float timeLastActive)
    {
        bool active = false;
        bool hasSpawned = false;

        foreach (Transform spawnedObject in objects)
        {
            if (spawnedObject.gameObject.active)
            {
                active = true;
            }
        }

        if (!active)
        {
            float randomNumber = Random.Range(0, objects.childCount);

            foreach (Transform spawnedObject in objects)
            {
                if (spawnedObject.GetSiblingIndex() == randomNumber)
                {
                    SpawnedObject chosenSP = spawnedObject.GetComponent<SpawnedObject>();

                   if (Time.time > timeLastActive + chosenSP.activeTime + chosenSP.respawnTime)
                    {
                        hasSpawned = true;
                        chosenSP.Respawn();
                        DelayedPointDespawn(chosenSP.activeTime, chosenSP);
                    }
                }
            }
        }

        return hasSpawned;
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

    
