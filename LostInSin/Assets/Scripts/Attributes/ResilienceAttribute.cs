using UnityEngine;

namespace LostInSin.Attributes
{
    public class ResilienceAttribute : Attribute
    {
        private ResilienceAttribute()
        {
            _maxValue = 10f;
            _currentValue = 1;
        }
    }
}