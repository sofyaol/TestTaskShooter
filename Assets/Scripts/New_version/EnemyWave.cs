using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyWave : MonoBehaviour, IWave
{
   public WaveCondition WaveCondition { get; set; } = WaveCondition.Await;

   [SerializeField] private Waypoint _waypoint; 
   public Waypoint Waypoint => _waypoint;
   
   [SerializeField] private GameObject _enemyHolder;

   private List<Enemy> _enemies = new List<Enemy>();

   public event Action GoToNextWave;

   private int EnemyCount { get; set; }
   

   private void Start()
   {
      if (_enemyHolder == null)
      {
         _enemyHolder = transform.Find("EnemyHolder").gameObject;
      }

      // получить все дочерние объекты типа енеми из объекта енеми холдер
      foreach (var enemy in _enemyHolder.GetComponentsInChildren<Enemy>())
      {
         _enemies.Add(enemy);
         enemy.OnCharacterDie += OnCharacterDie;
      }

      if (_waypoint == null)
      {
         _waypoint = GetComponentInChildren<Waypoint>();
      }

      EnemyCount = _enemies.Count;
   }

   // подписан на смерть врага в данной волне 
   private void OnCharacterDie()
   {
      EnemyCount--;

      if (EnemyCount == 0)
      {
         WaveCondition = WaveCondition.Passed;
         GoToNextWave?.Invoke();
      }
   }

   public void StartWaveAttack()
   {
      foreach (var enemy in _enemies)
      {
         enemy.StartAttack();
      }
   }

  
}
