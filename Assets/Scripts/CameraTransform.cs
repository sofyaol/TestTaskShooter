using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
   [SerializeField] GameObject player;
   [SerializeField] Vector3 diff;
    [SerializeField] Vector3 diff2;

    internal bool IsFollowingPlayer { get; set; } = true;

    void Update()
    {
        if (IsFollowingPlayer)
        {
            transform.position = player.transform.position + diff;
        }
    }
}
