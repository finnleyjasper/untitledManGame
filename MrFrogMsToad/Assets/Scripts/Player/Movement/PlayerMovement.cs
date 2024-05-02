/* 
 * filename: PlayerMovement.cs
/* author: Finnley Newnham
/* description: processes credentials sent from the apply.php form
/*
/* created: 30 April 2024
/* last modified: 30 April 2024
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
   public Rigidbody2D rigidbody2D { get; private set; }
}
