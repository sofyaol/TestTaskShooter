using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
   [SerializeField] PlayerState player;
   [SerializeField] WaypointController waypointController;
   [SerializeField] PlayerShoot playerShoot;


    void Update()
    {

        if (Input.touchCount > 0)
        {
            if (player.playerCondition == PlayerState.PlayerCondition.Stay && PhaseController.EnemyPhase == EnemyPhaseEnum.First)
            {
                player.RunCondition();
                waypointController.MakeMove1();
            }

            else if(player.playerCondition == PlayerState.PlayerCondition.Shoot)
                {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0f) + new Vector3(0, 0, 6f));
                    playerShoot.MakeShoot(touchPosition);
                }

                }
          
        }

#if UNITY_EDITOR

        if (Input.GetKey("space"))
        {
            if (player.playerCondition == PlayerState.PlayerCondition.Stay && PhaseController.EnemyPhase == EnemyPhaseEnum.First)
            {
                player.RunCondition();
                waypointController.MakeMove1();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            if (player.playerCondition == PlayerState.PlayerCondition.Shoot)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
                playerShoot.MakeShoot(mousePosition);
            }
            
        }

#endif
    }
}
