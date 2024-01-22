using System;
using LostInSin.Characters;
using UnityEngine;

namespace LostInSin.Abilities
{
    public struct AbilityTarget : IEquatable<AbilityTarget>
    {
        public Vector3 Point;
        public Character Character;

        public static bool operator ==(AbilityTarget obj1, AbilityTarget obj2) => obj1.Equals(obj2);

        public static bool operator !=(AbilityTarget obj1, AbilityTarget obj2) => !(obj1 == obj2);

        public bool Equals(AbilityTarget other) => Point.Equals(other.Point) ||
                                                   Equals(Character, other.Character);

        public override bool Equals(object obj) => obj is AbilityTarget other &&
                                                   Equals(other);

        public override int GetHashCode() => HashCode.Combine(Point, Character);
    }
}