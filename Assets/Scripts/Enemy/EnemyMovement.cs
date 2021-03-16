using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    EnemyAreaTrigger enemyAreaTrigger;
    [SerializeField] Transform endPoint;
    [SerializeField] float duration;
    PlayerState playerState;
    Animator animator;

    bool IsMoving { get; set; } = false;

    void Start()
    {
        enemyAreaTrigger = GetComponent<EnemyAreaTrigger>();
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (enemyAreaTrigger.isAttack && !IsMoving && playerState.playerCondition == PlayerState.PlayerCondition.Shoot) 
        {
            IsMoving = true;
            transform.DOMove(endPoint.position, duration);
            animator.SetBool("IsAttack", true);
        }
    }
}
