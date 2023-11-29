using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDeteciton : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = .8f;
    [SerializeField] private float fadeTime = .4f;
    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(spriteRenderer){
            StartCoroutine(FadeRoutine(spriteRenderer.color.a, transparencyAmount, fadeTime, spriteRenderer));
        } else if (tilemap){
            StartCoroutine(FadeRoutine(tilemap.color.a, transparencyAmount, fadeTime, tilemap));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(spriteRenderer){
            StartCoroutine(FadeRoutine(spriteRenderer.color.a, 1f, fadeTime, spriteRenderer));
        }else if (tilemap){
            StartCoroutine(FadeRoutine(tilemap.color.a, 1f, fadeTime, tilemap));
        }
    }

    private IEnumerator FadeRoutine(float startValue, float targetTransparency, float fadeTime, SpriteRenderer spriteRenderer){
        float elapsedTime = 0;

        while(elapsedTime < fadeTime){
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime/fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(float startValue, float targetTransparency, float fadeTime, Tilemap tilemap){
        float elapsedTime = 0;

        while(elapsedTime < fadeTime){
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime/fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
