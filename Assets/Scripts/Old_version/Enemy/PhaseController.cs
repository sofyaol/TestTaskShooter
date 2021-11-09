using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhaseController
{
    // is managed by EnemyCondition 

    internal static EnemyPhaseEnum EnemyPhase { get; private set; } = EnemyPhaseEnum.First;

    static int _maxEnemyCount = 2;

    static int _deadEnemyCountFirst = 0;
    static int _deadEnemyCountSecond = 0;
    static int _deadEnemyCountThird = 0;
    
    static GameObject _player;
    static PlayerState _playerState;
    static WaypointController _waypointController;
   

    internal static void NextPhase(EnemyPhaseEnum phase)  // is called by EnemyCondition 
    {
        if (phase == EnemyPhaseEnum.First)
        {
            _deadEnemyCountFirst++;

            if (_deadEnemyCountFirst == _maxEnemyCount)
            {
                EnemyPhase = EnemyPhaseEnum.Second;

                // init fields

                _player = GameObject.Find("Player");
                _playerState = _player.GetComponent<PlayerState>();
                _waypointController = _player.GetComponent<WaypointController>();

                // make player run to waypoint
                _playerState.playerCondition = PlayerState.PlayerCondition.Run;
                _waypointController.MakeMove1(2);


            }
        }

        else if (phase == EnemyPhaseEnum.Second)
        {
            _deadEnemyCountSecond++;

            if (_deadEnemyCountSecond == _maxEnemyCount)
            {
               
                EnemyPhase = EnemyPhaseEnum.Third;
                _playerState.playerCondition = PlayerState.PlayerCondition.Run;
                _waypointController.MakeMove1(4);
            }
        }

        else if (phase == EnemyPhaseEnum.Third)
        {
            PhaseStartCondition();
            _playerState.playerCondition = PlayerState.PlayerCondition.Run;
            _waypointController.MakeMove1(5);
        }
    }

    internal static void PhaseStartCondition()
    {
        EnemyPhase = EnemyPhaseEnum.First;
        _deadEnemyCountFirst = 0;
        _deadEnemyCountSecond = 0;
        _deadEnemyCountThird = 0;
    }
}

enum EnemyPhaseEnum
{
    First,
    Second,
    Third,
}
