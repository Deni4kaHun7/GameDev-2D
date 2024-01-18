using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string portalName;

    private void Start() {
        if(portalName == SceneManagment.Instance.PortalToSpawn){
            PlayerController.Instance.transform.position = gameObject.transform.position;
            CameraController.Instance.SetCinemachineFollowPlayer();
        }
    }
}
