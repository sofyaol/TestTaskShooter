using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationTrigger : MonoBehaviour
{
    [SerializeField] AnimationEnum animationEnum = 0;
    PlayerState playerState;

    void Start()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (animationEnum == AnimationEnum.shooting)
            {
                playerState.ShootCondition();
            }

            if (animationEnum == AnimationEnum.jumping)
            {
               
                playerState.JumpCondition();
            }

            if (animationEnum == AnimationEnum.finish)
            {
                playerState.FinishCondition();
            }

        }
    }

    private enum AnimationEnum
    {
        shooting,
        jumping,
        finish,
    }
}
