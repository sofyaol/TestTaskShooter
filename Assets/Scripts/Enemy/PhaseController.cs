using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhaseController
{
    // is managed by EnemyCondition 

    internal static EnemyPhaseEnum EnemyPhase { get; private set; } = EnemyPhaseEnum.First;

    static int maxEnemyCount = 2;

    static int deadEnemyCountFirst = 0;
    static int deadEnemyCountSecond = 0;
    static int deadEnemyCountThird = 0;
    
    static GameObject player;
    static PlayerState playerState;
    static WaypointController waypointController;
   

    internal static void NextPhase(EnemyPhaseEnum phase)  // is called by EnemyCondition 
    {
        if (phase == EnemyPhaseEnum.First)
        {
            deadEnemyCountFirst++;

            if (deadEnemyCountFirst == maxEnemyCount)
            {
                EnemyPhase = EnemyPhaseEnum.Second;

                // init fields

                player = GameObject.Find("Player");
                playerState = player.GetComponent<PlayerState>();
                waypointController = player.GetComponent<WaypointController>();

                // make player run to waypoint
                playerState.playerCondition = PlayerState.PlayerCondition.Run;
                waypointController.MakeMove1(2);


            }
        }

        else if (phase == EnemyPhaseEnum.Second)
        {
            deadEnemyCountSecond++;

            if (deadEnemyCountSecond == maxEnemyCount)
            {
               
                EnemyPhase = EnemyPhaseEnum.Third;
                playerState.playerCondition = PlayerState.PlayerCondition.Run;
                waypointController.MakeMove1(4);
            }
        }

        else if (phase == EnemyPhaseEnum.Third)
        {
            PhaseStartCondition();
            playerState.playerCondition = PlayerState.PlayerCondition.Run;
            waypointController.MakeMove1(5);
        }
    }

    internal static void PhaseStartCondition()
    {
        EnemyPhase = EnemyPhaseEnum.First;
        deadEnemyCountFirst = 0;
        deadEnemyCountSecond = 0;
        deadEnemyCountThird = 0;
    }
}

enum EnemyPhaseEnum
{
    First,
    Second,
    Third,
}
