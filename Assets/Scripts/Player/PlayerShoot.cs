using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] float rotateDuration = 0.1f;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletDuration;
    Vector3 target;

    internal void MakeShoot(Vector3 target) // is called in TouchManager
    {
        this.target = target;
        Debug.DrawLine(transform.position, target, Color.red);
        float angle = -Vector2.SignedAngle(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));
            angle = angle < 0 ? angle - 20f : angle;
            transform.DORotate(new Vector3(transform.rotation.x, transform.position.y + angle, transform.rotation.z), rotateDuration, RotateMode.Fast)
                .SetEase(Ease.Linear).OnComplete(ShootAnim);
       
    }

    private void ShootAnim()
    {
        playerAnimator.SetBool("IsShooting", true);
        StartCoroutine("StopShootAnim");
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.transform.DOMove(target, bulletDuration).OnComplete(()=>Destroy(bullet));
    }

    IEnumerator StopShootAnim()
    {
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("IsShooting", false);
    }
}
