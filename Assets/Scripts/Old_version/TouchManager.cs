using System.Collections;
using System.Collections.Generic;
using New_version;
using UnityEngine;

public class TouchManager : Singleton<TouchManager>
{
   Player _player;

   private Shooting _playerShooting;

   void Start()
   {
       _player = Player.Instance;
       _playerShooting = Shooting.Instance;
   }

   void Update()
   {

       
       if (Input.touchCount > 0)
       {
           if (!_player.MoveToNextPoint)
           {
               Touch touch = Input.GetTouch(0);
               if (touch.phase == TouchPhase.Began)
               {
                   Vector3 touchPosition =
                       Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0f) +
                                                      new Vector3(0, 0, 6f));
                   _playerShooting.MakeShoot(touchPosition);
               }
           }

       }


#if UNITY_EDITOR

       

       if (Input.GetMouseButtonDown(0))
       {

           if (!_player.MoveToNextPoint)
           {
               Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
               _playerShooting.MakeShoot(mousePosition);
           }

       }
   }
#endif
}

