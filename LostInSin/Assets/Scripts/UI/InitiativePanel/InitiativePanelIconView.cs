using System;
using LostInSin.Characters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace LostInSin.UI.InitiativePanel
{
    public class InitiativePanelIconView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [Inject] private InitiativePanelVM _initiativePanelVM;

        private void Setup(RectTransform parent, Character character)
        {
            transform.SetParent(parent);
            transform.localScale = Vector3.one;

            _image.sprite = character.CharacterPersistentData.CharacterAvatar;
        }

        public class Pool : MonoMemoryPool<RectTransform, Character, InitiativePanelIconView>
        {
            protected override void Reinitialize(RectTransform parent, Character character, InitiativePanelIconView view)
            {
                view.Setup(parent, character);
            }
        }
    }
}