using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private float knockBackThrust = 15f;

    private int currentHealth;
    private Knockback knockback;
    private DamageFlash damageFlash;
    

    private void Awake() {
        knockback = GetComponent<Knockback>();
        damageFlash = GetComponent<DamageFlash>();
    }
    private void Start() {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        Debug.Log(currentHealth);
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(damageFlash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    public IEnumerator CheckDetectDeathRoutine(){
        yield return new WaitForSeconds(damageFlash.GetDamageFlashTime());
        DetectDeath();
    }

    public void DetectDeath() {
        if (currentHealth <= 0){
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
