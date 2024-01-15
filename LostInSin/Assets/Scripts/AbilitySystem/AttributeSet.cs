using System;
using LostInSin.Attributes;
using LostInSin.Identifiers;
using UnityEngine;
using Zenject;

namespace LostInSin.AbilitySystem
{
    public class AttributeSet
    {
        [Inject(Id = AttributeIdentifiers.Health)]
        private IAttribute _healthAttribute;

        [Inject(Id = AttributeIdentifiers.Power)]
        private IAttribute _powerAttribute;

        [Inject(Id = AttributeIdentifiers.Resilience)]
        private IAttribute _resilienceAttribute;

        [Inject(Id = AttributeIdentifiers.Luck)]
        private IAttribute _luckAttribute;

        public IAttribute GetAttribute(AttributeIdentifiers identifier)
        {
            return identifier switch
                   {
                       AttributeIdentifiers.Health => _healthAttribute,
                       AttributeIdentifiers.Power => _powerAttribute,
                       AttributeIdentifiers.Resilience => _resilienceAttribute,
                       AttributeIdentifiers.Luck => _luckAttribute,
                       _ => throw new ArgumentException($"Unknown attribute identifier: {identifier}")
                   };
        }
    }
}