using UnityEngine;

namespace LostInSin.Attributes
{
    public class InitiativeAttribute : Attribute
    {
        private InitiativeAttribute()
        {
            _maxValue = 10f;
            _currentValue = Random.Range(1f, 10f); //FOR testing
        }
    }
}