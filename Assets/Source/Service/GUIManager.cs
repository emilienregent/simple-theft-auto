using System;
using System.Collections.Generic;
using Simple.Service;
using Simple.Context;
using UnityEngine;
using UnityEngine.UI;
using Simple.GUI;
using Simple.Behaviour;
using Simple.CustomType;

namespace Simple.Manager
{
    public class GUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _canvasPrefab = null;

        private Dictionary<int, Canvas> _playerIdToCanvas = new Dictionary<int, Canvas>();
        private Dictionary<int, Text> _textIdToTexts = new Dictionary<int, Text>();

        private static GUIManager _instance = null;

        public static GUIManager instance
        { 
            get
            { 
                return _instance;
            }
            set
            { 
                _instance = value;
            }
        }

        private void Awake()
        {
            GUIManager.instance = this;
        }

        public void BindGUIToPlayer(AbstractPlayer player)
        {
            GameObject canvasGO = GameObject.Instantiate<GameObject>(_canvasPrefab);

            Canvas canvas = canvasGO.GetComponent<Canvas>();

            _playerIdToCanvas.Add(player.id, canvas);

            UniqueText[] texts = canvas.GetComponentsInChildren<UniqueText>(true);

            for (int j = 0; j < texts.Length; j++)
            {
                _textIdToTexts.Add(GetUniqueKey(player, texts[j].type), texts[j].GetComponent<Text>());
            }
        }

        private int GetUniqueKey(AbstractPlayer player, UniqueTextType type)
        {
            return player.id * 1000 + (int)type;
        }

        public Text GetUniqueTextForPlayer(AbstractPlayer player, UniqueTextType type)
        {
            int index = GetUniqueKey(player, type);

            if (_textIdToTexts.ContainsKey(index) == true)
            {
                return _textIdToTexts[index];
            }

            throw new UnityException("Can't find unique text " + type + " for player " + player.id);
        }
    }
}