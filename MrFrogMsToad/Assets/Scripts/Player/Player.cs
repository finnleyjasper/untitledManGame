using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class Player : MonoBehaviour
{
    public PlayerMovement movement { get; private set; }
    [SerializeField] private Collider2D collider;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public int _score;
    public int _lives;

    private void Awake()
    {
        this.movement = GetComponent<PlayerMovement>();
        this.collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        ChangeDirection();
    }


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
        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
        }
    }

    public static explicit operator Player(GameObject v)
    {
        throw new NotImplementedException();
    }
}
