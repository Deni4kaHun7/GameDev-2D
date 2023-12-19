using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private string nextSceneEntranceToSpawn;

    private void OnTriggerEnter2D(Collider2D other) {
        SceneManagment.Instance.SetPortalToSpawn(nextSceneEntranceToSpawn);
        SceneManager.LoadScene(nextScene);
    }
}
