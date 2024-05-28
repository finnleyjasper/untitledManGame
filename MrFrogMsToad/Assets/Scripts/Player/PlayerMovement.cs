/* 
* filename: PlayerMovement.cs
* author: Finnley
* description: movement for the player. change's their transform.
*
* reference(s) - https://youtu.be/TKt_VlMn_aA
*
* created: 02 May 2024
* last modified:  02 May 2024
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody2D { get; private set; }

    public float speed = 0.8f;
    public float speedMultiplier = 1.0f;
    public Vector2 initalDirection;
    public LayerMask mapBottom;

    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initalDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;

        this.rigidbody2D.isKinematic = false;
        this.enabled = true; //ie. when gamestate == gameover, movement will be disabled. want to be sure its enabled when resetting state
    }

    private void Update()
    {
        if(this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }    
    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody2D.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;

        this.rigidbody2D.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false) // forced == optional parameter for if the direction should be forced
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 attemptedDirection)
    {
        // * 0.75 so it is slightly smaller than the box colliders of the map, 0.0f is angle which doesnt matter, and 1.5f == checking 1 unit
        // from our position, + 1/5 as the cast is done from the centre of our position
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, attemptedDirection, 1.5f, this.mapBottom);

        // hit will be assigned a value if the boxcast hits something, it will be null if the space is free
        return hit.collider != null;
    }
}

