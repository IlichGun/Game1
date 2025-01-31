using Game1.Shooting;
using UnityEngine;

namespace Game1.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        public Weapon _weaponPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(_weaponPrefab);
        }
    }
}