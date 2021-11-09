using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishWave : Singleton<FinishWave>, IWave
{
  [SerializeField] private Waypoint _waypoint;
   public Waypoint Waypoint => _waypoint;
   public WaveCondition WaveCondition { get; set; } = WaveCondition.Await;
   public event Action GoToNextWave;
}
