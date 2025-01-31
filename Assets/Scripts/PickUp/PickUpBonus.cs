using Game1.Bonus;
using UnityEngine;

namespace Game1.PickUp
{
    public class PickUpBonus : PickUpItem
    {
        [SerializeField]
        public BonusSpeed _bonusSpeedPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetBonus(_bonusSpeedPrefab);
        }
    }
}