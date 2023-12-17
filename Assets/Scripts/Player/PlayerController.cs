using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{   
    public bool FacingLeft {get { return facingLeft; }}

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private TrailRenderer dashTrailRenderer;
    
    private PlayerControls playerControls;
    //Store values incoming from our players input
    private Vector2 movement; 
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    private bool facingLeft = false;
    private bool isDashing = false;
    private float startingMoveSpeed;
    private Knockback knockback;

    private void Start() {
        playerControls.Movement.Dash.performed += _ => Dash();    
        startingMoveSpeed = movementSpeed;
    }

    protected override void Awake() {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
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
        AdjustPlayerFaceDirection();
        Move();
    }

    private void PlayerInput () {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move(){
        //if player gets knockedback, he cannot move
        if(knockback.GettingKnockedBack){ return ;}
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
            mySpriteRender.flipX = true;
            facingLeft = true;
        } else{
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash(){
        if(!isDashing){
            isDashing = true;
            dashTrailRenderer.emitting = true;
            movementSpeed *= dashSpeed;
            StartCoroutine(EndDashRoutine());
        }
        
    }

    private IEnumerator EndDashRoutine(){

        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        movementSpeed = startingMoveSpeed;
        dashTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
