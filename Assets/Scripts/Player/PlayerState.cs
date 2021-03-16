using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] internal PlayerCondition playerCondition = 0;

    void Start()
    {
        playerAnimator.SetBool("IsRunning", false);
    }

    internal void RunCondition()
    {
        playerCondition = PlayerCondition.Run;
        playerAnimator.SetBool("IsRunning", true);  
    }

    internal void JumpCondition()
    {
        playerCondition = PlayerCondition.Jump;
        playerAnimator.SetBool("IsJumping", true);
        StartCoroutine("StopJumpAnim");

        transform.GetComponent<WaypointController>().MakeMove1(3);
    }

    internal void ShootCondition()
    {
        playerCondition = PlayerCondition.Shoot;
        playerAnimator.SetBool("IsRunning", false);
    }

    internal void FinishCondition()
    {
        playerCondition = PlayerCondition.Dance;
        playerAnimator.SetBool("IsDancing", true);
        StartCoroutine("RestartGame");
    }

    internal enum PlayerCondition
    {
        Stay,
        Run,
        Shoot,
        Jump,
        Dance,
    }

    IEnumerator StopJumpAnim()
    {
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("IsJumping", false);
        playerAnimator.SetBool("IsRunning", false);
    }

    IEnumerator RestartGame()
    {
       
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameScene");
    }
}