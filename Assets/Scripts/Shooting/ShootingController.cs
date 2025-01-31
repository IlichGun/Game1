using UnityEngine;

namespace Game1.Shooting
{
    public class ShootingController : MonoBehaviour
    {

        private GameObject _target;
        public bool HasTarget => _target != null;

        public Vector3 TargetPosition => _target.transform.position;

        private Weapon _weapon;


        private Collider[] _colliders = new Collider[2];

        private float _nextShootTimerSec;
 
        protected void Update()
        {
            _target = GetTarget();
            _nextShootTimerSec -= Time.deltaTime;
            if (_nextShootTimerSec < 0)
            {
                if (HasTarget)
                    _weapon.Shoot(TargetPosition);

                _nextShootTimerSec = _weapon.ShootFrequencySec;
            }
        }

        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            if (_weapon != null)
                Destroy(_weapon.gameObject);

            _weapon = Instantiate(weaponPrefab, hand); // Создание оружия в руке
            _weapon.transform.localPosition = Vector3.zero; // Позиция оружия относительно руки
            _weapon.transform.localRotation = Quaternion.identity;
        }

        private GameObject GetTarget()
        {
            GameObject tar = null;

            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;
            // var mask = LayerUtils.EnemyMask;
            int mask = 0;
            if (gameObject.layer == LayerMask.NameToLayer(LayerUtils.EnemyLayerName))
            {
                mask = LayerUtils.PlayerMask;
            }
            else if (gameObject.layer == LayerMask.NameToLayer(LayerUtils.PlayerLayerName))
            {
                mask = LayerUtils.EnemyMask;
            }

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);//Запускает сферу вокруг указанной позиции. Size - размер коллайдера(объектов в сфере: 0,1,2)
            if (size > 0)
            { 
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != gameObject) 
                    {
                        tar = _colliders[i].gameObject;
                        break;
                    }
                }
            }
            return tar;
        }
    }
}