using UnityEngine;
using UnityEngine.UIElements;

namespace Game1.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _n= 2f;
        [SerializeField]
        private float _maxRadiansDelta = 10f;

        public Vector3 MovementDirection {  get; set; } // Направление куда идем
        public Vector3 LookDirection { get; set; } // Идем на врага и смотрим на него

        private CharacterController _characterController;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();

            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
            {
                Rotate();
            }

        }

        private void Translate()
        {
            Vector3 delta;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                 delta = _n * MovementDirection * _speed * Time.deltaTime;
            }
            else
            {
                 delta = MovementDirection * _speed * Time.deltaTime;
            }
            
            _characterController.Move(delta);
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