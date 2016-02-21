using System;
using UnityEngine;
using Simple.CustomType;
using Simple.Context;
using Simple.Behaviour;
using System.Collections.Generic;

namespace Simple.Service
{
    public class CharacterSelectionService : AbstractCharacterService
    {
        private Dictionary<int, CharacterType> _playerIdToCharacterType = new Dictionary<int, CharacterType>();

        public override void Initialize()
        {
            SelectionContext selectionContext = _context as SelectionContext;

            for (int i = 0; i < selectionContext.characters.Length; i++)
            {
                GameObject gameObject = selectionContext.characters[i];
                Character character = gameObject.GetComponent<Character>();

                if (_selectableCharacterToGameObject.ContainsKey(character.type) == false)
                {
                    _selectableCharacterToGameObject.Add(character.type, gameObject);
                }
            }
        }

        public GameObject GetPreviousSelectableCharacter(AbstractPlayer player)
        {
            CharacterType type = _playerIdToCharacterType.ContainsKey(player.id) == true 
                ? _playerIdToCharacterType[player.id].Previous()
                : CharacterType.NONE.Previous();

            if (type == CharacterType.NONE)
                type = type.Previous();

            _playerIdToCharacterType[player.id] = type;

            return CreateCharacter(type);
        }

        public GameObject GetNextSelectableCharacter(AbstractPlayer player)
        {
            CharacterType type = _playerIdToCharacterType.ContainsKey(player.id) == true 
                ? _playerIdToCharacterType[player.id].Next()
                : CharacterType.NONE.Next();

            if (type == CharacterType.NONE)
                type = type.Next();

            _playerIdToCharacterType[player.id] = type;

            return CreateCharacter(type);
        }

        public GameObject CreateCharacter(CharacterType type)
        {
            return GameObject.Instantiate(_selectableCharacterToGameObject[type]);
        }
    }
}