using UnityEngine;

namespace LostInSin.Visuals
{
    public class CharacterVisualsVO : MonoBehaviour
    {
        [SerializeField] private GameObject _selectionDecal;
        public GameObject SelectionDecal => _selectionDecal;
    }
}