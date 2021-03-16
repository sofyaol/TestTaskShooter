using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAreaTrigger : MonoBehaviour
{
    [SerializeField] internal bool isAttack = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            isAttack = true;
        }

        
    }
}

