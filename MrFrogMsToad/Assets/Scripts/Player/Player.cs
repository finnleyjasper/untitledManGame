/* 
* filename: Player.cs
* author: Finnley
* description: the player class - takes input for movement script, stores score, lives, and manages "death"
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 02 May 2024
* last modified:  09 May 2024
*/

/* Notes:
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class Player : MonoBehaviour
{
    public PlayerMovement movement { get; private set; }
    [SerializeField] private Collider2D collider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public SpriteRenderer spriteRenderer;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    private int _score;
    private int _lives;
    private bool _isDead = false;
    private bool _canKill = false;
    private Vector3 startingPosition;


    public int startingLives;

    public Sprite normalSprite;
    public Sprite powerUpSprite;

    public IPowerup currentPowerup;

    private void Awake()
    {
        _lives = startingLives;
        startingPosition = gameObject.transform.position;

        movement = GetComponent<PlayerMovement>();
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChangeDirection();
    }

    // MOVEMENT
    private void ChangeDirection()
    {
        if (Input.GetKeyDown(up))
        {
            this.movement.SetDirection(Vector2.up);
            ChangeOrientation();
        }
        if (Input.GetKeyDown(down))
        {
            this.movement.SetDirection(Vector2.down);
            ChangeOrientation();
        }
        if (Input.GetKeyDown(left))
        {
            this.movement.SetDirection(Vector2.left);
            ChangeOrientation();
        }
        if (Input.GetKeyDown(right))
        {
            this.movement.SetDirection(Vector2.right);
            ChangeOrientation();
        }

       
    }

    private void ChangeOrientation()
    {
        rb.freezeRotation = false; // this is on by default so player's dont get spun around when clipping objects
        float angle = Mathf.Atan2(-this.movement.direction.x, this.movement.direction.y);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        rb.freezeRotation = true;
    }

    // GAME EVENTS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // PLAYER EATEN
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players") && _canKill)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            FindObjectOfType<GameMngr>().PlayerEaten(this, player);
        }
    }

    public void RemovePowerup()
    {
        if (currentPowerup !=null)
        {
            currentPowerup.RevertPowerup(this);
            currentPowerup = null;
        }
    }

    // VARIABLE UPDATING + CHECKING
    public void IncreaseScore(int increse)
    {
        _score += increse;
    }

    private void CheckCollider() // turns off/on collider depending on _isDead
    {
        if (_isDead)
        {
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true;
        }
    }

    public void LoseLife() // turns off this object, sets to dead
    {
        _lives -= 1;
        this.gameObject.SetActive(false);
        _isDead = true;
        CheckCollider();
    }

    // STATES
    public void Respawn()
    {
        movement.ResetState();
        this.gameObject.SetActive(true);
        _isDead = false;
        this.transform.position = startingPosition;
        BeInvincible();

    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
        movement.ResetState();
        this.transform.position = startingPosition;
        _score = 0;
        _lives = startingLives;
        _isDead = false;
        _canKill = false;
        currentPowerup = null;
        movement.speedMultiplier = 1f;
        CheckCollider();
    }

    // COROUTINES
    public void BeInvincible()
    {
        // coroutine code: https://discussions.unity.com/t/toggling-a-sprite-renderer/135413/2
        StartCoroutine("ToggleSpriteAndCollider");
    }

    IEnumerator ToggleSpriteAndCollider() // sprite will flash, collider will turn back on after
    {
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.3f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.3f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        CheckCollider();
    }

    public int Score
    {
        get
        { return _score; }
    }

    public int Lives
    {
        get
        { return _lives; }
    }

    public bool CanKill
    {
        get { return _canKill; }
        set { _canKill = value; }
    }
}
