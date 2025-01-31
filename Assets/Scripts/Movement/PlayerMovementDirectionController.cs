using Game1.Bonus;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game1.Movement
{
    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera _camera;

        public Vector3 MovementDirection {  get; private set; }

        private CharMovementController _characterMovementController;
        [SerializeField]
        private float _n = 2f;
        
        protected void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _characterMovementController = GetComponent<CharMovementController>();
        }

        protected void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector3(horizontal, 0, vertical);
            direction = _camera.transform.rotation * direction;
            direction.y = 0;

            MovementDirection = direction.normalized;
        }

        public void LShiftActive()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                 _characterMovementController.Delta = _n * MovementDirection * _characterMovementController.Speed * Time.deltaTime;
            }
            else
            {
                 _characterMovementController.Delta = MovementDirection * _characterMovementController.Speed * Time.deltaTime;
            }
        }
    }
}