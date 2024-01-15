using LostInSin.Characters.StateMachine;
using UnityEngine;
using UniRx;
using Zenject;
using System;
using LostInSin.Animation.Data;
using LostInSin.Identifiers;
using LostInSin.Signals;

namespace LostInSin.Animation
{
    public class AnimationStateChanger : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Animator _animator;
        private readonly AnimationHashes _animationHashes;
        private readonly CompositeDisposable _disposables = new();


        private AnimationStateChanger(SignalBus signalbus,
                                      Animator animator,
                                      AnimationHashes animationHashes)
        {
            _signalBus = signalbus;
            _animator = animator;
            _animationHashes = animationHashes;
        }

        public void Initialize()
        {
            _signalBus.GetStream<IAnimationChangeSignal>()
                      .Subscribe(x => ChangeAnimationState(x.AnimationStateChangeData))
                      .AddTo(_disposables);
        }

        private void ChangeAnimationState(AnimationStateChangeData animationData)
        {
            int animationHash = GetAnimationHash(ref animationData);

            Type parameterType = animationData.ParameterType;
            TypeCode typeCode = Type.GetTypeCode(parameterType);

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    _animator.SetBool(animationHash, animationData.GetAnimationParameter<bool>().Value);
                    break;
                case TypeCode.Decimal:
                    _animator.SetFloat(animationHash, animationData.GetAnimationParameter<float>().Value);
                    break;
                case TypeCode.Int32:
                    _animator.SetInteger(animationHash, animationData.GetAnimationParameter<int>().Value);
                    break;
                case TypeCode.Byte:
                    _animator.SetTrigger(animationHash);
                    break;
            }
        }

        private int GetAnimationHash(ref AnimationStateChangeData animationData)
        {
            AnimationIdentifier id = animationData.AnimationID;
            int animationHash = _animationHashes.GetAnimationHash(id);
            return animationHash;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}