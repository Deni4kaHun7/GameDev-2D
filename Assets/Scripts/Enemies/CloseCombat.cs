using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombat : MonoBehaviour, IEnemy
{
    public void Attack(){
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

         
    }
}
