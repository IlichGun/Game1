using UnityEngine;

namespace Game1
{
    public static class LayerUtils
    {
        public const string BulletLayerName = "Bullet"; // default player enemy -> 1 1 0 (слой дефолтный и слой игрока, но не враг)
        public const string EnemyLayerName = "Enemy"; // EnemyMask -> 0 0 1 -> побитовое перемножение -> 0 0 0 -> не относится к врагам
        public const string PlayerLayerName = "Player";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);

        public static readonly int EnemyMask = LayerMask.GetMask(EnemyLayerName);

        public static readonly int PlayerMask = LayerMask.GetMask(PlayerLayerName);
        public static bool IsBullet(GameObject other) => other.layer == BulletLayer; // Метод проверки попадания пули

       // public static bool IsBullet(GameObject other)
       // {
       //   return other.layer == BulletLayer;
       // }
    }
}
