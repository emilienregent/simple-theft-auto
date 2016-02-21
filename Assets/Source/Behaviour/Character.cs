using System;
using UnityEngine;
using Simple.CustomType;

namespace Simple.Behaviour
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterType _type = CharacterType.NONE;

        public CharacterType type { get { return _type; } }
    }
}