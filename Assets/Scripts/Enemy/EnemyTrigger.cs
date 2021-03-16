using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    EnemyCondition enemyCondition;
    Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        enemyCondition = GetComponentInParent<EnemyCondition>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet")
        {
            enemyCondition.Health--;

            if (enemyCondition.Health == 0)
            {
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsDead", true);
                enemy.transform.DOPause();
                gameObject.SetActive(false);
            }
            
        }
    }
}
