using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using New_version;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Animator),
    typeof(Rigidbody),
    typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, ICharacter
{
    public event Action OnCharacterDie;
    internal bool IsAlive { get; set; } = true;
    private EnemyState _enemyState = EnemyState.Await;

    private float _health = 100f;
    
    [Tooltip("Bullet damage to the enemy")]
    [SerializeField] private float _bulletDamage = 50f;
    
    [Tooltip("Enemy damage to the player")]
    [SerializeField] private float _enemyDamage = 50f;
    
    [SerializeField] private float _hitDelay = 5f;
    [SerializeField] private float _maxHitDistance = 0.4f;

    private NavMeshAgent _agent;
    private Player _player;
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");
    private static readonly int Fighting = Animator.StringToHash("Fighting");
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        
        _player = Player.Instance;
        _player.OnCharacterDie += StopAttack;
    }
    
    private void Update()
    {
        if (_enemyState == EnemyState.Await) return;
        
        var heading = _agent.transform.position - _player.transform.position;
        if (heading.sqrMagnitude < _maxHitDistance * _maxHitDistance) //сравниваем расстояние между врагом и игроком с максимальным
        {
            StopAttack();
            
            if (_enemyState != EnemyState.Hitting)//(hitAvailable == false)
            {
                _enemyState = EnemyState.Hitting;//hitAvailable = true;
                MakeHit();
            }
        }

        else
        {
            _enemyState = EnemyState.Attack;
            _agent.SetDestination(_player.transform.position);
           // hitAvailable = false;
        }
        
    }

    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
                Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == 6 /*Bullet*/ && IsAlive)
        {
            Health -= _bulletDamage;
            _agent.isStopped = true;
            _animator.SetBool(IsHit, true);
            
        }
    }

    void OnHitByPlayerEnd()
    {
        _agent.isStopped = false;
        _agent.enabled = true;
        _animator.SetBool(IsHit, false);
    }
    private void Die()
    {
        IsAlive = false;
        OnCharacterDie?.Invoke();
    }

    public void StartAttack()
    {
        _animator.SetBool(IsAttack, true);
        _enemyState = EnemyState.Attack;
        _agent.SetDestination(_player.transform.position);
    }

    private void StopAttack()
    {
        _agent.isStopped = true;
    }
    
    private void MakeHit()
    {
        if (!IsAlive || _player.Health <= 0 || _enemyState != EnemyState.Hitting) return; // ||!hitAvailable
        _animator.SetBool(IsAttack, false);
        _animator.SetBool(Fighting, true);
      
    }

    private void OnHitByEnemyEnd()
    {
        _player.GetDamage(_enemyDamage);
        StartCoroutine(StartHitDelay());
        _animator.SetBool(Fighting, false);
    }

    private void OnHitByEnemyMiddle()
    {
        _player.gameObject.GetComponent<ColorGlow>().MakeColorGlare();
    }
    private IEnumerator StartHitDelay()
    {
        yield return new WaitForSeconds(_hitDelay);
        MakeHit();
    }
    
}
