using Simple.Service;
using System.Collections.Generic;
using UnityEngine;
using Simple.CustomType;

namespace Simple.Context
{
    public class SelectionContext : AbstractContext
    {
        [SerializeField]
        private GameObject _playerPrefab = null;
        [SerializeField]
        private GameObject[] _characters = new GameObject[0];

        public GameObject playerPrefab { get { return _playerPrefab; } }
        public GameObject[] characters { get { return _characters; } }

        public override void BindServices()
        {
            RegisterService<CharacterSelectionService>(new CharacterSelectionService());
            RegisterService<InputService>(new InputService());
            RegisterService<AbstractPlayerService>(new PlayerSelectionService());
        }
    }
}