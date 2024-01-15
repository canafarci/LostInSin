using UnityEngine;

namespace LostInSin.Attributes
{
    public class LuckAttribute : IAttribute
    {
        private float _maxValue = 10f;
        private float _currentValue = 1f;

        public float MaxValue => _maxValue;

        public float CurrentValue => _currentValue;

        public void SetValue(float value)
        {
            _currentValue = value;
        }

        public void AddToValue(float change)
        {
            _currentValue = Mathf.Clamp(_currentValue += change, 0f, _maxValue);
        }

        public float GetValue()
        {
            return _currentValue;
        }
    }
}