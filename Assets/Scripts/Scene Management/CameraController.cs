using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : Singleton<CameraController>
{
   private CinemachineVirtualCamera cinemachineVirtualCamera;
   
   public void SetCinemachineFollowPlayer()
   {
    cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    Debug.Log("dsdsd");
    cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
   }
}
