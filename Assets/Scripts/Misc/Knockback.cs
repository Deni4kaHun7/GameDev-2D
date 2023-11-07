using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool GettingKnockedBack
     {get ; private set;}
    [SerializeField] private float knockBackTime = .1f;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    /*damageSource - the position of the player when he attacks the enemy
    knockBackThrust - with what force the attack occur
    */
    public void GetKnockedBack(Transform damageSource, float knockBackThrust){ 
        /*(transform.position - damageSource.position) - where enemy should move after hit
        normalize - in every direction moves the same
        knockBackThrust - the speed of the movement
        rb.mass - add mass so lighter objects move faster, heavier slower */
        GettingKnockedBack
         = true;
        Vector2 differnce = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        /*difference - the direction of the impulse
        ForceMode2D.Impulse - type of Force*/
        rb.AddForce(differnce, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine(){
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockedBack
         = false;
    }
}
