using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Visuals
{
    public class CharacterVisualsVO : MonoBehaviour
    {
        [SerializeField] private GameObject _selectionDecal;
        public GameObject SelectionDecal { get { return _selectionDecal; } }
    }
}
