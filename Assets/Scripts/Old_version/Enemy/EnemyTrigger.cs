using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    EnemyCondition _enemyCondition;
    Animator _animator;

    void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _enemyCondition = GetComponentInParent<EnemyCondition>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet")
        {
            _enemyCondition.Health--;

            if (_enemyCondition.Health == 0)
            {
                _animator.SetBool("IsAttack", false);
                _animator.SetBool("IsDead", true);
                enemy.transform.DOPause();
                gameObject.SetActive(false);
            }
            
        }
    }
}
