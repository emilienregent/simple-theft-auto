using Simple.Service;
using System.Collections.Generic;
using UnityEngine;
using Simple.CustomType;
using Simple.Behaviour;

namespace Simple.Context
{
	public class GameContext : AbstractContext
	{
        [SerializeField]
        private GameObject _playerPrefab = null;
        [SerializeField]
        private GameObject _canvasPrefab = null;
        [SerializeField]
        private GameObject[] _characters = new GameObject[0];
        [SerializeField]
        private GameObject[] _weapons = new GameObject[0];

        private Dictionary<CharacterType, Character> _typeToPlayableCharacter = new Dictionary<CharacterType, Character>();
        private Dictionary<WeaponType, Weapon> _typeToPlayableWeapon = new Dictionary<WeaponType, Weapon>();

        public GameObject playerPrefab { get { return _playerPrefab; } }
        public GameObject canvasPrefab { get { return _canvasPrefab; } }
        public GameObject[] characters { get { return _characters; } }

        public override void Initialize()
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                Character character = _characters[i].GetComponent<Character>();

                if (_typeToPlayableCharacter.ContainsKey(character.type) == false)
                {
                    _typeToPlayableCharacter.Add(character.type, character);
                }
            }

            for (int i = 0; i < _weapons.Length; i++)
            {
                Weapon weapon = _weapons[i].GetComponent<Weapon>();

                if (_typeToPlayableWeapon.ContainsKey(weapon.type) == false)
                {
                    _typeToPlayableWeapon.Add(weapon.type, weapon);
                }
            }
        }

        public override void BindServices()
        {
            RegisterService<InputService>(new InputService());
            RegisterService<CharacterGameService>(new CharacterGameService());
            RegisterService<AbstractPlayerService>(new PlayerGameService());
        }

        public GameObject GetPlayableCharacter(CharacterType type)
        {
            return _typeToPlayableCharacter[type].gameObject;
        }
	}
}