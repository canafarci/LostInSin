using System;
using LostInSin.Abilities.AbilityData.Abstract;
using LostInSin.Animation;
using LostInSin.Characters;
using LostInSin.Identifiers;
using LostInSin.Signals.Animations;
using UnityEngine;

namespace LostInSin.Abilities.AbilityData.CastingStarter
{
    [CreateAssetMenu(fileName = "Play Animation Ability Start", menuName = "LostInSin/Abilities/Casting Start/Play Animation Casting Start", order = 0)]
    public class AnimationPlayCastingStart : AbilityCastingStarter
    {
        public AnimationIdentifier abilityID;
        public TypeHint AnimationTypeHint;
        public float Value;
        
        public override void StartCasting(Character instigator)
        {
            AnimationChangeSignalBuilder signalBuilder = new AnimationChangeSignalBuilder();

            switch (AnimationTypeHint)
            {
                case TypeHint.Boolean:
                    signalBuilder.SetAnimationParameter(Value > 0);
                    break;
                case TypeHint.Float:
                    signalBuilder.SetAnimationParameter(Value);
                    break;
                case TypeHint.Int:
                    signalBuilder.SetAnimationParameter((int)Value);
                    break;
                case TypeHint.Trigger: //type hint for trigger
                    signalBuilder.SetAnimationParameter(new byte());
                    break;
                default:
                    throw new Exception($"No type has found to build animation signal on {instigator}!");
            }
            
            AnimationChangeSignal animationChangeSignal = signalBuilder.SetAnimationIdentifier(abilityID).Build();
            
            instigator.SignalBus.AbstractFire(animationChangeSignal);
        }
    }
}