using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float knockBackThrust = 10f;
    [SerializeField] private float damageRecoveryCD = 1f;
    private Knockback knockback;
    private DamageFlash flash;
    private float currentHealth;
    private bool canTakeDamage = true;

    private void Awake() {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<DamageFlash>();
    }

    private void Start() {
        currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if(enemy && canTakeDamage){
            TakeDamage(1);
            knockback.GetKnockedBack(other.gameObject.transform, knockBackThrust);
            StartCoroutine(flash.FlashRoutine());
        }
    }

    private void TakeDamage(int damageAmount){
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        Debug.Log(currentHealth);
    }

    private IEnumerator DamageRecoveryRoutine(){
        yield return new WaitForSeconds(damageRecoveryCD);
        canTakeDamage = true;
    }
}
