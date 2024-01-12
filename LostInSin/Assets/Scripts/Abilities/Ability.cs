using System.Collections;
using System.Collections.Generic;
using LostInSin.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Abilities
{
    public abstract class Ability : SerializedScriptableObject
    {
        public abstract bool CanCast();
        public abstract void CastAbility(Character target);
    }
}