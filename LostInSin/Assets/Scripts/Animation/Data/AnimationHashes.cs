using System.Collections.Generic;
using LostInSin.Identifiers;
using UnityEngine;

namespace LostInSin.Animation.Data
{
    public class AnimationHashes
    {
        private Data _data;
        private Dictionary<AnimationIdentifier, int> _animationHashLookup = new();

        private AnimationHashes(Data data)
        {
            _data = data;
        }

        public int GetAnimationHash(AnimationIdentifier animationID)
        {
            if (_animationHashLookup.ContainsKey(animationID))
            {
                return _animationHashLookup[animationID];
            }
            else
            {
                int animationHash = CreateHash(animationID);

                _animationHashLookup[animationID] = animationHash;

                return animationHash;
            }
        }

        private int CreateHash(AnimationIdentifier animationID)
        {
            string animationKey = _data.AnimationKeyIDPairs[animationID];
            int animationHash = Animator.StringToHash(animationKey);
            return animationHash;
        }

        public class Data
        {
            public Dictionary<AnimationIdentifier, string> AnimationKeyIDPairs;
        }
    }
}