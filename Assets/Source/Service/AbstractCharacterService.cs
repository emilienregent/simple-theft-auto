using System;
using System.Collections.Generic;
using Simple.CustomType;
using UnityEngine;

namespace Simple.Service
{
    public abstract class AbstractCharacterService : AbstractService
    {
        protected Dictionary<CharacterType, GameObject> _selectableCharacterToGameObject = new Dictionary<CharacterType, GameObject>();
    }
}