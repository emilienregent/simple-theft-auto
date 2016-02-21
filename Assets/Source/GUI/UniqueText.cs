using System;
using UnityEngine;
using UnityEngine.UI;
using Simple.CustomType;

namespace Simple.GUI
{
    [RequireComponent(typeof(Text))]
    public class UniqueText : MonoBehaviour
    {
        [SerializeField]
        private UniqueTextType _type = UniqueTextType.NONE;

        public UniqueTextType type { get { return _type; } }
    }
}

