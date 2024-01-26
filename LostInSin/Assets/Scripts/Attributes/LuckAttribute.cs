using UnityEngine;

namespace LostInSin.Attributes
{
    public class LuckAttribute : Attribute
    {
        private LuckAttribute()
        {
            _maxValue = 10f;
            _currentValue = 1;
        }
    }
}