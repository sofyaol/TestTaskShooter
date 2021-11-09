using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : Singleton<StartWave>, IWave
{
    [SerializeField] private Waypoint _waypoint;
    public Waypoint Waypoint => _waypoint;

    public WaveCondition WaveCondition { get; set; } = WaveCondition.Await;
    
    public event Action GoToNextWave;

    void Update()
    {
        if (WaveCondition == WaveCondition.Await && true /* был тач*/)
        {
            WaveCondition = WaveCondition.Passed;
            GoToNextWave?.Invoke();
        }
    }
}
