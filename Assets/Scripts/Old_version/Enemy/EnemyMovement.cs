using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    EnemyAreaTrigger _enemyAreaTrigger;
    [SerializeField] Transform endPoint;
    [SerializeField] float duration;
    PlayerState _playerState;
    Animator _animator;

    bool IsMoving { get; set; } = false;

    void Start()
    {
        _enemyAreaTrigger = GetComponent<EnemyAreaTrigger>();
        _playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        _animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (_enemyAreaTrigger.isAttack && !IsMoving && _playerState.playerCondition == PlayerState.PlayerCondition.Shoot) 
        {
            IsMoving = true;
            transform.DOMove(endPoint.position, duration);
            _animator.SetBool("IsAttack", true);
        }
    }
}
