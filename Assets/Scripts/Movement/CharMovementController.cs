using UnityEngine;

namespace Game1.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        [SerializeField]
        public float Speed = 2f;
        [SerializeField]
        private float _maxRadiansDelta = 10f;

        public Vector3 MovementDirection {  get; set; } // Направление куда идем
        public Vector3 LookDirection { get; set; } // Идем на врага и смотрим на него

        private CharacterController _characterController;
        private PlayerMovementDirectionController _movementDirectionController;
        public Vector3 Delta { get; set; }

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _movementDirectionController = GetComponent<PlayerMovementDirectionController>();
        }

        protected void Update()
        {

            Translate();

            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
            {
                Rotate();
            }
            _movementDirectionController.LShiftActive();
        }

        private void Translate()
        {
            _characterController.Move(Delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitudeeee = (currentLookDirection - LookDirection).sqrMagnitude;

            if (sqrMagnitudeeee > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadiansDelta * Time.deltaTime);

                transform.rotation = newRotation;
            }
        }
    }
}