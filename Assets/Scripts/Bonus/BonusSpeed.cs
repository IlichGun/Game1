using Game1.Movement;
using Unity.VisualScripting;
using UnityEngine;

namespace Game1.Bonus
{
    public class BonusSpeed : MonoBehaviour
    {
        [SerializeField]
        private float _n = 3f; // Множитель ускорения
        [SerializeField]
        private float _bonusSpeedTimer = 2f; // Время действия ускорения
        private float _initialSpeed;
        private CharMovementController _characterMovementController;
        private bool _isBonusActive = false;
        protected void Awake()
        {
            _characterMovementController = GetComponent<CharMovementController>();
            _isBonusActive = false;
        }

        public void TakeBonus(CharMovementController speed)
        {

            if (_isBonusActive == false) // Проверка наличия активного баффа скорости на персонаже
            {
                _characterMovementController = speed;
                _initialSpeed = _characterMovementController.Speed;
                _characterMovementController.Speed = _initialSpeed * _n;
                _isBonusActive = true;
            }

            CancelInvoke(nameof(ResetSpeed)); // При повторном взятии бонуса, если время с предыдущего не прошло, обновляем время действия бонуса до _bonusSpeedTimer
            Invoke(nameof(ResetSpeed), _bonusSpeedTimer);
        }
       
        private void ResetSpeed()
        {
            if (_characterMovementController != null)
            {
                _characterMovementController.Speed = _initialSpeed;
                _isBonusActive = false;
            }  
        }
    }
}