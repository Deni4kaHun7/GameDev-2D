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
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    [SerializeField] Vector3 m;


    private void Awake() {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }
    
    //Better works with Input systems
    private void Update() {
        PlayerInput();
        m = Input.mousePosition;
    }

    //Better works with Physics
    private void FixedUpdate() {
        AdjustPlayerFaceDirection();
        Move();
    }

    private void PlayerInput () {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move(){
        /*rb.position = current player position 
        movement = for how long  player want to move
        put movementSpeed and time into brackets so floats calculate only once
        */
        rb.MovePosition(rb.position + movement * ( movementSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFaceDirection(){
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePosition.x < playerScreenPoint.x){
            Debug.Log("dfs");
            mySpriteRender.flipX = true;
        } else{
            mySpriteRender.flipX = false;
            Debug.Log("faslse");
        }
    }
}
