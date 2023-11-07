using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float knockBackThrust = 10f;
    [SerializeField] private float damageRecoveryCD = 1f;
    [SerializeField] private Image[] heartsList;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    
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
        UpdateHealthUI();
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
        UpdateHealthUI();
    }

    private IEnumerator DamageRecoveryRoutine(){
        yield return new WaitForSeconds(damageRecoveryCD);
        canTakeDamage = true;
    }

    private void UpdateHealthUI(){
        for(int i = 0; i < heartsList.Length; i++){
            if(i < currentHealth){
                heartsList[i].sprite = fullHeart;
            } else {
                heartsList[i].sprite = emptyHeart;
            }
        }
    }
}
