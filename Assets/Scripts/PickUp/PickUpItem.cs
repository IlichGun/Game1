using System;
using UnityEngine;

namespace Game1.PickUp
{
    public abstract class PickUpItem : MonoBehaviour
    {
        
        public event Action<PickUpItem> OnPickedUp;

        public virtual void PickUp(BaseCharacter character)
        {
            OnPickedUp?.Invoke(this);
        }
    }
}