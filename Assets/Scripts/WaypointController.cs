using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaypointController : MonoBehaviour
{
    // С этим скриптом следовало бы создать отдельный объект WaypointController на сцене, но 
    // в таком случае проблемы с библиотекой DoTween ;(

    [SerializeField] PlayerState playerState;

    Vector3[][] spots = new Vector3[5][];
    Vector3[] cameraSpots;
    
    [SerializeField] List<Transform> path0;
    [SerializeField] List<Transform> path1;
    [SerializeField] List<Transform> path2;
    [SerializeField] List<Transform> path3;
    [SerializeField] List<Transform> path4;

    [SerializeField] List<Transform> cameraPath;

    [SerializeField] float[] durations;    

    void Start()
    {
        spots[0] = new Vector3[path0.Count];
        spots[1] = new Vector3[path1.Count];
        spots[2] = new Vector3[path2.Count];
        spots[3] = new Vector3[path3.Count];
        spots[4] = new Vector3[path4.Count];

        cameraSpots = new Vector3[cameraPath.Count];

        for (int i = 0; i < cameraPath.Count; i++)
        {
            cameraSpots[i] = cameraPath[i].transform.position;
        }

        SpotsInitialization(0, path0, path0.Count);
        SpotsInitialization(1, path1, path1.Count);
        SpotsInitialization(2, path2, path2.Count);
        SpotsInitialization(3, path3, path3.Count);
        SpotsInitialization(4, path4, path4.Count);

    }

    private void SpotsInitialization(int pathNumber, List<Transform> path, int spotsCount)
    {
        for (int i = 0; i < spotsCount; i++)
        {
            spots[pathNumber][i] = path[i].transform.position;
        }
    }

    internal void MakeMove1(int pathPhase = 1)
    {
        pathPhase = pathPhase - 1;

        switch (pathPhase)
        {
            case 0:

                transform.DOPath(spots[0], durations[0]).SetEase(Ease.Linear).OnComplete(CompleteMove1);
                break;

            case 1:

                playerState.RunCondition();
                transform.DOPath(spots[1], durations[1], PathType.Linear).SetEase(Ease.Linear).OnComplete(CompleteMove2);
                transform.DORotate(new Vector3(0, 10f, 0), durations[1] - 1f).SetEase(Ease.Linear);

                break;

            case 2:
              
                transform.DOMove(spots[2][1], 2.2f);
                transform.DORotate(new Vector3(0, 0, 0), 2f);
                break;

            case 3:

                playerState.RunCondition();
                transform.DOPath(spots[3], durations[3]).SetEase(Ease.Linear);
                transform.DORotate(new Vector3(0, 0, 0), 2f);

                break;

            case 4:

                playerState.RunCondition();
                transform.DOPath(spots[4], durations[4]).SetEase(Ease.Linear);
                transform.DORotate(new Vector3(0, 0, 0), 2f);
                Camera.main.GetComponent<CameraTransform>().IsFollowingPlayer = false;
                Camera.main.transform.DOPath(cameraSpots, 5f).SetEase(Ease.Linear);
                Camera.main.transform.DORotate(new Vector3(18.392f, 179.99f, 0f), 5f).SetEase(Ease.Linear);
                break;
        }
    }

    private void CompleteMove1()
    {
        playerState.playerCondition = PlayerState.PlayerCondition.Shoot;
    }

    private void CompleteMove2()
    {
        playerState.playerCondition = PlayerState.PlayerCondition.Jump;
    }

    private void CompleteMove3()
    {
        playerState.playerCondition = PlayerState.PlayerCondition.Run;
    }

}
