using System;
using UnityEngine;

    public interface IWave
    { 
    Waypoint Waypoint { get; }
    WaveCondition WaveCondition { get; set; }
    
    public event Action GoToNextWave;
    }
