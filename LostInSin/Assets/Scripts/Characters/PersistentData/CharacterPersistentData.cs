using Cysharp.Threading.Tasks;
using LostInSin.Abilities;
using Sirenix.OdinInspector;
using UnityEngine;
using LostInSin.Identifiers;
using Sirenix.Utilities;
using Zenject;

namespace LostInSin.Characters.PersistentData
{
    [CreateAssetMenu(fileName = "CharacterPersistentData", menuName = "Characters", order = 0)]
    public class CharacterPersistentData : SerializedScriptableObject
    {
        public bool DefaultSelectedCharacter = false;
        public GameObject CharacterPrefab;

        public string CharacterName;
        public Sprite CharacterAvatar;

        public CharacterTeam CharacterTeam;
        public CharacterClass CharacterClass;

        public AbilityInfo[] CharacterAbilities;
        public AbilityInfo MoveAbility;

        public async void Inject(DiContainer container)
        {
            await UniTask.NextFrame(); //wait one frame for other dependencies to be injected, as SOs are loaded first

            CharacterAbilities.ForEach(x =>
                                       {
                                           container.Inject(x.AbilityBlueprint);
                                           x.AbilityBlueprint.Initialize();
                                       });
            container.Inject(MoveAbility.AbilityBlueprint);
        }
    }
}