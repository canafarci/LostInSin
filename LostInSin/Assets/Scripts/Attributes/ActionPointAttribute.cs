using UnityEngine;

namespace LostInSin.Attributes
{
    public class ActionPointAttribute : Attribute
    {
        private ActionPointAttribute()
        {
            _maxValue = 7f;
            _currentValue = 7f;
        }
    }
}