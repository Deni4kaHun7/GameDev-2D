using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{   
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float attackCD = .5f;

    private PlayerControls playerControls;
    private Animator animator;
    [SerializeField] private Transform weaponCollider;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;
    private bool attackButtonDown, isAttacking = false;

    private void Awake(){
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void StartAttacking(){
        attackButtonDown = true;
    }

    private void StopAttacking(){
        attackButtonDown = false;
    }

    private void Update() {
        MouseFollowWithOffset();
        Attack();
    }

    private void Attack(){
        //Switches the trigger in animations to start the animation
        if(attackButtonDown && !isAttacking){
            isAttacking = true;
            animator.SetTrigger("Attack");
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            weaponCollider.gameObject.SetActive(true);
            StartCoroutine(AttackCDRoutine());
        } 
        
    }

    private IEnumerator AttackCDRoutine(){
        yield return new WaitForSeconds(attackCD);
        isAttacking = false;
    }

    //Flips animation on 180 degrees so when swing up flip anim activates this aniamtion works backwards because it was flipped 
    public void SwingUpFlipAnim(){
        slashAnim.transform.rotation = Quaternion.Euler(-180 ,0 ,0);
        
        if (playerController.FacingLeft){
            slashAnim.GetComponent<SpriteRenderer>().flipX = true; 
        }
    }

    public void SwindDownFlipAnim(){
        slashAnim.transform.rotation = Quaternion.Euler(0 ,0 ,0);
        
        if (playerController.FacingLeft){
            slashAnim.GetComponent<SpriteRenderer>().flipX = true; 
        }
    }

    //Turnes off the weapon collider after he finishes attack
    public void DoneAttackingAnimEvent(){
        weaponCollider.gameObject.SetActive(false);
    }

    private void MouseFollowWithOffset(){
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);
        float  angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if(mousePosition.x < playerScreenPoint.x){
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
