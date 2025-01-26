using UnityEngine;

namespace Game1.Shooting
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] // Для "непростых" полей
        public Bullet BulletPrefab {  get; private set; }

        [field: SerializeField]
        public float ShootRadius { get; private set; } = 5f; // Дистанция стрельбы

        [field: SerializeField]
        public float ShootFrequencySec { get; private set; } = 1f; // Частота стрельбы

        [SerializeField]
        private float _damage = 1f;


        [SerializeField]
        private float _bulletMaxFlyDistance = 10f; // Максимальная дистанция полета пули

        [SerializeField]
        private float _bulletFlySpeed = 10f; // Скорость пули (по дефолту 10)

        [SerializeField]
        private Transform _bulletSpawnPosition;

        public void Shoot(Vector3 targetPoint) // Заставляет оружие стрелять
        {
            var bullet = Instantiate(BulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);

            var target = targetPoint - _bulletSpawnPosition.position; // Куда пуля должна лететь
            target.y = 0;
            target.Normalize();

            bullet.Initialize(target, _bulletMaxFlyDistance, _bulletFlySpeed, _damage);

        }
    }
}