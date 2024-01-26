using UnityEngine;

namespace LostInSin.Attributes
{
    public class HealthAttribute : Attribute
    {
        private HealthAttribute()
        {
            _maxValue = 100f;
            _currentValue = 50f;
        }
    }
}