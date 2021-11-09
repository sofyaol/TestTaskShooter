using UnityEngine;

namespace New_version
{
    [CreateAssetMenu(menuName = "Data/Bullet Data", fileName = "new Bullet Data")]
    public class BulletData : ScriptableObject
    {
        internal Rigidbody Rb { get; set; }
        internal Vector3 Direction { get; set; }

    }
}