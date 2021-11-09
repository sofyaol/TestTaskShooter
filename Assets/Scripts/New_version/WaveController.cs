using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public delegate void WaveAction(IWave wave);
public class WaveController : Singleton<WaveController>
{
    [SerializeField] private List<IWave> _waves = new List<IWave>();

    public event WaveAction OnNextWave;
    internal IWave CurrentWave { get; set; }

    void Start()
    {
        foreach (var wave in GetComponentsInChildren<IWave>())
        {
            _waves.Add(wave);
            wave.GoToNextWave += OnGoToNextWave;
        }

        CurrentWave = _waves[0];
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        IWave lastWave = null;
        
        foreach (var wave in _waves)
        {
            if (lastWave != null)
            {
                Gizmos.DrawLine(lastWave.Waypoint.transform.position, wave.Waypoint.transform.position);
            }

            lastWave = wave;
        }

    }

    public void OnGoToNextWave()
    {
        _waves.RemoveAt(0);
        CurrentWave = _waves[0];
        OnNextWave?.Invoke(CurrentWave);
    }

    public void StartCurrentWaveAttack()
    {
        var enemyWave = CurrentWave as EnemyWave;
        enemyWave?.StartWaveAttack();
    }
    

}
