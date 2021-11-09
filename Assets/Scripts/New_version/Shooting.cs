using UnityEngine;
using DG.Tweening;

namespace New_version
{
    public class Shooting : Singleton<Shooting>
    {
        private Player _player;
        [SerializeField] private Transform _bulletSpawn;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _duration = 10f;
        
        void Start()
        {
            _player = Player.Instance;

        }

        public void MakeShoot(Vector3 target)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawn);
            bullet.transform.position = _bulletSpawn.transform.position;
            bullet.transform.LookAt(target);
            bullet.transform.DOMove(target, _duration).OnComplete(() => bullet.SetActive(false));
            _player.ShootAnimation(target);
        }
    }
}
