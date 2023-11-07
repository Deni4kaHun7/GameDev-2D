using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;

    public void Awake(){
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }
    public void FixedUpdate(){
        if(knockback.GettingKnockedBack){return;}

        rb.MovePosition(rb.position + moveDir * (movementSpeed * Time.deltaTime));
    }
    public void Move(Vector2 targetedDir){
        moveDir = targetedDir;
    }
}
