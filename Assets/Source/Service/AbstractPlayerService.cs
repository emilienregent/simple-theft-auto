using System;
using Simple.CustomType;
using Simple.Context;
using System.Collections.Generic;
using UnityEngine;
using Simple.Behaviour;
using GamepadInput;

namespace Simple.Service
{
    public abstract class AbstractPlayerService : AbstractService
    {
        protected InputService _inputService = null;
        protected List<AbstractPlayer> _players = new List<AbstractPlayer>();

        public List<AbstractPlayer> players { get { return _players; } }

        public override void Initialize()
        {
            _inputService = _context.GetService<InputService>();
        }

        protected abstract void CreatePlayer(int id);

        public AbstractPlayer GetPlayer(int id)
        {
            return id < _players.Count ? _players[id] : null;
        }

        public void ExecuteInputFromSource(InputType input, InputSource source, InputState state)
        {
            if (source.id >= _players.Count)
            {
                throw new UnityException("Can't find player #" + source.id);
            }

            ExecuteInput(_players[source.id], input, state);
        }

        protected abstract void ExecuteInput(AbstractPlayer player, InputType input, InputState state);
    }
}