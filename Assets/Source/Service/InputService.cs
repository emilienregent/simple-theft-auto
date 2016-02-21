using GamepadInput;
using Simple.CustomType;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Simple.Service
{
    public class InputService : AbstractService
	{
		private List<InputSource> _sources = new List<InputSource>();
        private AbstractPlayerService _playerService = null;

        public List<InputSource> sources { get { return _sources; } }

		public override void Initialize()
		{                
            // Add one for the keyboard
            InputSource source = new InputSource(InputSourceType.KEYBOARD);
            source.id = 0;
            _sources.Add(source);

            // Get input source from gamepads
			string[] names = Input.GetJoystickNames();

			for(int i = 0; i < names.Length; i++)
			{
				source = new InputSource(InputSourceType.GAMEPAD);

                source.id = i + 1;

				_sources.Add(source);
            }
        }

        public override void Start()
        {
            _playerService = _context.GetService<AbstractPlayerService>();
        }

        public override void Update()
        {
            for (int i = 0; i < _sources.Count; i++)
            {
                InputSource source = _sources[i];

                if (source.type == InputSourceType.GAMEPAD)
                {
                    ListenGamepadSource(source);
                }
                else if (source.type == InputSourceType.KEYBOARD)
                {
                    ListenKeyboardSource(source);
                }
            }
        }

        private void ListenGamepadSource(InputSource source)
        {
        }

        private void ListenKeyboardSource(InputSource source)
        {
            if (Input.GetButtonDown("Left"))
                _playerService.ExecuteInputFromSource(InputType.LEFT, source, InputState.PRESSED);
            else if (Input.GetButtonUp("Left"))
                _playerService.ExecuteInputFromSource(InputType.LEFT, source, InputState.RELEASED);
            else if (Input.GetButton("Left"))
                _playerService.ExecuteInputFromSource(InputType.LEFT, source, InputState.STAY);

            if (Input.GetButtonDown("Up"))
                _playerService.ExecuteInputFromSource(InputType.UP, source, InputState.PRESSED);
            else if (Input.GetButtonUp("Up"))
                _playerService.ExecuteInputFromSource(InputType.UP, source, InputState.RELEASED);
            else if (Input.GetButton("Up"))
                _playerService.ExecuteInputFromSource(InputType.UP, source, InputState.STAY);

            if (Input.GetButtonDown("Right"))
                _playerService.ExecuteInputFromSource(InputType.RIGHT, source, InputState.PRESSED);
            else if (Input.GetButtonUp("Right"))
                _playerService.ExecuteInputFromSource(InputType.RIGHT, source, InputState.RELEASED);
            else if (Input.GetButton("Right"))
                _playerService.ExecuteInputFromSource(InputType.RIGHT, source, InputState.STAY);

            if (Input.GetButtonDown("Down"))
                _playerService.ExecuteInputFromSource(InputType.DOWN, source, InputState.PRESSED);
            else if (Input.GetButtonUp("Down"))
                _playerService.ExecuteInputFromSource(InputType.DOWN, source, InputState.RELEASED);
            else if (Input.GetButton("Down"))
                _playerService.ExecuteInputFromSource(InputType.DOWN, source, InputState.STAY);

            if (Input.GetButtonDown("Submit"))
                _playerService.ExecuteInputFromSource(InputType.START, source, InputState.PRESSED);
            else if (Input.GetButtonUp("Submit"))
                _playerService.ExecuteInputFromSource(InputType.START, source, InputState.RELEASED);
            else if (Input.GetButton("Submit"))
                _playerService.ExecuteInputFromSource(InputType.START, source, InputState.STAY);

            if (Input.GetButtonDown("Jump"))
                _playerService.ExecuteInputFromSource(InputType.ACTION_1, source, InputState.PRESSED);
            else if (Input.GetButtonUp("Jump"))
                _playerService.ExecuteInputFromSource(InputType.ACTION_1, source, InputState.RELEASED);
            else if (Input.GetButton("Jump"))
                _playerService.ExecuteInputFromSource(InputType.ACTION_1, source, InputState.STAY);

            if (Input.GetButtonDown("Fire1"))
                _playerService.ExecuteInputFromSource(InputType.TRIGGER_RIGHT, source, InputState.PRESSED);
            else if (Input.GetButton("Fire1"))
                _playerService.ExecuteInputFromSource(InputType.TRIGGER_RIGHT, source, InputState.STAY);
            else if (Input.GetButtonUp("Fire1"))
                _playerService.ExecuteInputFromSource(InputType.TRIGGER_RIGHT, source, InputState.RELEASED);
            
            if (Input.GetButtonDown("Fire2"))
                _playerService.ExecuteInputFromSource(InputType.TRIGGER_LEFT, source, InputState.PRESSED);
            else if (Input.GetButtonUp("Fire2"))
                _playerService.ExecuteInputFromSource(InputType.TRIGGER_LEFT, source, InputState.RELEASED);
        }
	}
}