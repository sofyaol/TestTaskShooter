using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using New_version;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class Player : Singleton<Player>, ICharacter
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _health = 100;
    public int Health{ 
        get => _health;
        set
        {
            _health = value;
            
            if (_health <= 0)
            {
                Die();
            }
        } 
    }
    
   private NavMeshAgent _agent;
   public bool MoveToNextPoint { get; set; }

   private WaveController _waveController;
   private static readonly int IsRunning = Animator.StringToHash("IsRunning");
   private static readonly int Death = Animator.StringToHash("Death");
   private static readonly int IsShooting = Animator.StringToHash("IsShooting");

   public event Action OnCharacterDie;

   void Awake()
    {
        transform.position = StartWave.Instance.Waypoint.transform.position;
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _waveController = WaveController.Instance;
        _waveController.OnNextWave += RunningToNextWave;
        

    }

    private void RunningToNextWave(IWave wave)
    {
        Waypoint waypoint = wave.Waypoint;
        _agent.SetDestination(wave.Waypoint.transform.position);
        MoveToNextPoint = true;
        _animator.SetBool(IsRunning, true);
    }
    
    private void MakeWaveActive()
    {
        _waveController.StartCurrentWaveAttack();
    }

    private void Update()
    {
        if (_agent.pathEndPosition == transform.position && MoveToNextPoint)
        {
            MakeWaveActive();
            MoveToNextPoint = false;
            _animator.SetBool(IsRunning, false);
        }
    }

    public void GetDamage(float damage)
    {
        Health -= (int)damage;
    }

    private void Die()
    {
        _animator.SetTrigger(Death);
        OnCharacterDie?.Invoke();
    }

    public void ShootAnimation(Vector3 target)
    {
        _animator.SetBool(IsShooting, true);
        transform.LookAt(target);
    }

    public void OnShootingStop()
    {
        _animator.SetBool(IsShooting, false);
    }
}
