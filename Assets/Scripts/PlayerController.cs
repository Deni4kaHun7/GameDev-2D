using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private PlayerControls playerControls;
    //Store values incoming from our players input
    private Vector2 movement; 
    private Rigidbody2D rb;

    private void Awake() {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }
    
    //Better works with Input systems
    private void Update() {
        PlayerInput();
    }

    //Better works with Physics
    private void FixedUpdate() {
        Move();
    }

    private void PlayerInput () {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void Move(){
        /*rb.position = current player position 
        movement = for how long  player want to move
        put movementSpeed and time into brackets so floats calculate only once
        */
        rb.MovePosition(rb.position + movement * ( movementSpeed * Time.fixedDeltaTime));
    }
}
