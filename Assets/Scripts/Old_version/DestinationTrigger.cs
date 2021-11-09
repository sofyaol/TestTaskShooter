using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationTrigger : MonoBehaviour
{
    [SerializeField] AnimationEnum animationEnum = 0;
    PlayerState _playerState;

    void Start()
    {
        _playerState = GameObject.Find("Player").GetComponent<PlayerState>();
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (animationEnum == AnimationEnum.Shooting)
            {
                _playerState.ShootCondition();
            }

            if (animationEnum == AnimationEnum.Jumping)
            {
               
                _playerState.JumpCondition();
            }

            if (animationEnum == AnimationEnum.Finish)
            {
                _playerState.FinishCondition();
            }

        }
    }

    private enum AnimationEnum
    {
        Shooting,
        Jumping,
        Finish,
    }
}
