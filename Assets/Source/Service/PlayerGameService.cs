using System;
using Simple.CustomType;
using Simple.Context;
using System.Collections.Generic;
using UnityEngine;
using Simple.Behaviour;
using Simple.Manager;

namespace Simple.Service
{
    public class PlayerGameService : AbstractPlayerService
    {
        private CharacterGameService _characterService = null;

        public override void Initialize()
        {
            base.Initialize();

            _characterService = _context.GetService<CharacterGameService>();

            for (int i = 0; i < _inputService.sources.Count; i++)
            {
                CreatePlayer(i);
            }
        }  

        protected override void CreatePlayer(int id)
        {
            GameContext game = _context as GameContext;
            
            GameObject playerPrefab = GameObject.Instantiate(game.playerPrefab);
            
            PlayerGame player = playerPrefab.GetComponent<PlayerGame>();
            
            player.id = id;
            
            _players.Add(player);

            player.SetRoot(_characterService.GetCharacterOfType(player.LoadCharacterType()));

            GUIManager.instance.BindGUIToPlayer(player as AbstractPlayer);
        }

        protected override void ExecuteInput(AbstractPlayer abstractPlayer, InputType input, InputState state)
        {
            PlayerGame player = abstractPlayer as PlayerGame;

            if (state == InputState.PRESSED)
            {
                switch (input)
                {
                    case InputType.TRIGGER_LEFT: 
                        player.Aim(true);
                        break;
                    case InputType.TRIGGER_RIGHT: 
                        player.Shoot(true);
                        break;
                }
            }
            else if (state == InputState.STAY)
            {
                switch (input)
                {
                    case InputType.LEFT: 
                        player.TurnLeft();
                        break;
                    case InputType.UP: 
                        player.MoveForward();
                        break;
                    case InputType.RIGHT: 
                        player.TurnRight();
                        break;
                    case InputType.DOWN: 
                        player.MoveBackward();
                        break;
                    case InputType.TRIGGER_RIGHT: 
                        player.Shoot(true);
                        break;
                }
            }
            else if (state == InputState.RELEASED)
            {
                switch (input)
                {
                    case InputType.ACTION_1: 
                        player.Jump();
                        break;
                    case InputType.TRIGGER_LEFT: 
                        player.Aim(false);
                        break;
                    case InputType.TRIGGER_RIGHT: 
                        player.Shoot(false);
                        break;
                }
            }
        }
    }
}