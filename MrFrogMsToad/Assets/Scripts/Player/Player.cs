/* 
* filename: Player.cs
* author: Finnley
* description: 
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 02 May 2024
* last modified:  07 May 2024
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

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode _powerupKey;

    private int _score;
    private int _lives;

    public int startingLives;
    public Vector3 statingPosition;

    private bool _hasPowerup = false;
    private bool _isDead = false;

    private void Awake()
    {
        _lives = startingLives;

        this.movement = GetComponent<PlayerMovement>();
        this.collider = GetComponent<Collider2D>();
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ChangeDirection();
        GetPowerup();
    }

    // MOVEMENT
    private void ChangeDirection()
    {
        if (Input.GetKeyDown(up))
        {
            this.movement.SetDirection(Vector2.up);
        }
        if (Input.GetKeyDown(down))
        {
            this.movement.SetDirection(Vector2.down);
        }
        if (Input.GetKeyDown(left))
        {
            this.movement.SetDirection(Vector2.left);
        }
        if (Input.GetKeyDown(right))
        {
            this.movement.SetDirection(Vector2.right);
        }

        ChangeOrientation();
    }

    private void ChangeOrientation()
    {
        rb.freezeRotation = false; // this is on by default so player's dont get spun around when clipping objects
        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        rb.freezeRotation = true;
    }

    // GAME EVENTS

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // PLAYER EATEN
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players") && _hasPowerup == true)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            FindObjectOfType<GameMngr>().PlayerEaten(this, player);
        }
    }

    // TEMP CODE FOR CHECKING PVP WORKS
    private void GetPowerup()
    {
        if (Input.GetKeyDown(_powerupKey))
        {
            if(_hasPowerup)
            {
                _hasPowerup = false;
            }
            else
            {
                _hasPowerup = true;
                Debug.Log(_hasPowerup);
            }
        }
    }

    private void CheckCollider() // turns off/on collider depending on _isDead
    {
        Collider2D collider = GetComponent<Collider2D>();

        if (_isDead)
        {
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true;
        }
    }

    // VARIABLE UPDATING
    public void IncreaseScore(int increse)
    {
        _score += increse;
    }

    public void Reset()
    {
        _score = 0;
        _lives = startingLives;
        _isDead = false;
        CheckCollider();
    }

    public void Respawn()
    {
        this.gameObject.SetActive(true);
        _isDead = false;
        CheckCollider();
        this.transform.position = statingPosition;
    }


    public void BeInvincible()
    {
        // coroutine code: https://discussions.unity.com/t/toggling-a-sprite-renderer/135413/2
        StartCoroutine("ToggleRenderer");
    }

    IEnumerator ToggleRenderer()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void LoseLife() // turns off this object, sets to dead
    {
        _lives -= 1;
        this.gameObject.SetActive(false);

        _isDead = true;
        CheckCollider();
    }

    public int Score
    {
        get
        {
            return _score;
        }
    }

    public int Lives
    {
        get
        {
            return _lives;
        }
    }

    public static explicit operator Player(GameObject v)
    {
        throw new NotImplementedException();
    }
}
