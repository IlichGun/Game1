using UnityEditor;
using UnityEngine;


namespace Game1.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem _pickUpPrefab;

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private int _maxCount = 2;

        [SerializeField]
        private float _maxSpawnIntervalSeconds = 5f;

        [SerializeField]
        private float _minSpawnIntervalSeconds = 1f;

        private float _currentSpawnTimerSeconds; // Сколько временипрошло с момента спавна предыдущего оружия
        private int _currentCount; // Текущее кол-во предметов, которое заспавнилось

        private float _averrageSpawnTime;

        private void Start()
        {
            _averrageSpawnTime = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }

        private void Update()
        {   
            if (_currentCount < _maxCount)
            {
                _currentSpawnTimerSeconds += Time.deltaTime; // Накопление таймера
                if (_currentSpawnTimerSeconds > _averrageSpawnTime)
                {
                   
                    _currentSpawnTimerSeconds = 0f;
                    _currentCount++;
                    _averrageSpawnTime = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);

                    var randomPointInsideRange = Random.insideUnitCircle * _range; // Получаем рандомную точку внтури круга и умножаем на _range чтобы получить указанный радиус
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position; // Получаем рандомную позицию 
                    
                    var pickUp =  Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;
                }
            }
        }

        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color; // В Handels Текущий цвет гизмосов
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range); // Рисуем зону спавна в центром текущей позиции с нормалю Y и радиусом _range
            Handles.color = cashedColor; // Сохраняем цвет для того, чтобы гизмосы рисовались своими цветами после того как нарисовали радиус.
        }
    }
}