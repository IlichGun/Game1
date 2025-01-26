using UnityEngine;

namespace Game1.Movement
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get; }
    }
}
