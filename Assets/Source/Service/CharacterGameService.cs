using System;
using UnityEngine;
using Simple.CustomType;
using Simple.Context;
using Simple.Behaviour;
using System.Collections.Generic;

namespace Simple.Service
{
    public class CharacterGameService : AbstractCharacterService
    {
        private Dictionary<CharacterSpawnPoint, Character> _spawnPointToCharacter = new Dictionary<CharacterSpawnPoint, Character>();

        public override void Initialize()
        {
            CharacterSpawnPoint[] spawnPoints = Transform.FindObjectsOfType<CharacterSpawnPoint>();

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                GameObject gameObject = spawnPoints[i].body;
                Character character = spawnPoints[i].character;

                _spawnPointToCharacter.Add(spawnPoints[i], character);

                if (_selectableCharacterToGameObject.ContainsKey(character.type) == false)
                {
                    _selectableCharacterToGameObject.Add(character.type, gameObject);
                }
            }
        }

        public GameObject GetCharacterOfType(CharacterType type)
        {
            if (_selectableCharacterToGameObject.ContainsKey(type) == false)
            {
                return ReplaceNonPlayableCharacter(type);
            }
            else
            {
                return _selectableCharacterToGameObject[type];
            }
        }

        public GameObject ReplaceNonPlayableCharacter(CharacterType type)
        {
            foreach (KeyValuePair<CharacterSpawnPoint, Character> pair in _spawnPointToCharacter)
            {
                if (pair.Value.type == CharacterType.NONE)
                {
                    GameContext gameContext = _context as GameContext;

                    return pair.Key.CreateCharacter(gameContext.GetPlayableCharacter(type));
                }
            }

            throw new UnityException("Can't find spawn point to create character of type " + type);
        }
    }
}