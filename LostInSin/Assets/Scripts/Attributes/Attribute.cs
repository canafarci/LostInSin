using UnityEngine;

namespace LostInSin.Attributes
{
    public class Attribute : IAttribute
    {
        protected float _maxValue;
        protected float _currentValue;

        public float MaxValue => _maxValue;
        public float CurrentValue => _currentValue;

        public void SetValue(float value)
        {
            _currentValue = value;
        }

        public void SetMaxValue(float value)
        {
            _maxValue = value;
        }

        public void AddToValue(float change)
        {
            _currentValue = Mathf.Clamp(_currentValue += change, 0f, _maxValue);
            Debug.Log($"Attribute : {this}, Current Value : {_currentValue}");
        }

        public void AddToMaxValue(float change)
        {
            _maxValue += change;
            Debug.Log($"Attribute : {this}, Max Value : {_currentValue}");
        }

        public float GetValue() => _currentValue;
        public float GetMaxValue() => _maxValue;
    }
}