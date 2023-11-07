using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{   
    [SerializeField] private Material damageFlashMat;
    [SerializeField] private float damageFlashTime = .2f;
    private Material defaultMat; 
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public float GetDamageFlashTime(){
        return damageFlashTime;
    }
    public IEnumerator FlashRoutine(){
        spriteRenderer.material = damageFlashMat;
        yield return new WaitForSeconds(damageFlashTime);
        spriteRenderer.material = defaultMat;
    }
}
