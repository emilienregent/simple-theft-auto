using System;
using Simple.CustomType;
using Simple.Context;
using System.Collections.Generic;
using UnityEngine;
using Simple.Behaviour;
using GamepadInput;

namespace Simple.Service
{
    public class PlayerSelectionService : AbstractPlayerService
    {
        private CharacterSelectionService _characterService = null;
        private SelectionContext _selectionContext = null;

        public override void Initialize()
        {
            base.Initialize();

            _selectionContext = _context as SelectionContext;
            _characterService = _context.GetService<CharacterSelectionService>();

            for (int i = 0; i < _inputService.sources.Count; i++)
            {
                CreatePlayer(i);
            }
        }

        protected override void CreatePlayer(int id)
        {
            GameObject playerPrefab = GameObject.Instantiate<GameObject>(_selectionContext.playerPrefab);

            PlayerSelection player = playerPrefab.GetComponent<PlayerSelection>();

            player.id = id;

            _players.Add(player);

            player.SetRoot(_characterService.GetNextSelectableCharacter(player));
        }

        protected override void ExecuteInput(AbstractPlayer player, InputType input, InputState state)
        {
            if (state == InputState.PRESSED)
            {
                switch (input)
                {
                    case InputType.LEFT: 
                        player.SetRoot(_characterService.GetPreviousSelectableCharacter(player));
                        break;
                    case InputType.RIGHT: 
                        player.SetRoot(_characterService.GetNextSelectableCharacter(player));
                        break;
                    case InputType.START:
                        PlayerSelection playerSelection = player as PlayerSelection;

                        playerSelection.SaveCharacterType();

                        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
                        break;
                }
            }
        }
    }
}