using UnityEngine;

namespace LostInSin.Attributes
{
    public class PowerAttribute : Attribute
    {
        private PowerAttribute()
        {
            _maxValue = 10f;
            _currentValue = 1;
        }
    }
}