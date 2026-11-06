using System.Collections.Generic;

namespace FoGD.Shark.States.Bank
{
    public enum SharkAggressionStates
    {
        Aggressive,
        Passive,
        Stunned,
        Cooldown
    }

    public class SharkAggressionStateBank
    {
        private Dictionary<SharkAggressionStates, SharkAggressionState> _states;

        public SharkAggressionStateBank(SharkBehaviour context)
        {
            _states = new Dictionary<SharkAggressionStates, SharkAggressionState>
            {
                { SharkAggressionStates.Aggressive, new SharkAggressiveState(context, this) },
                { SharkAggressionStates.Passive, new SharkPassiveState(context, this) },
                { SharkAggressionStates.Stunned, new SharkStunnedState(context, this) },
                { SharkAggressionStates.Cooldown, new SharkCooldownState(context, this) }
            };
        }

        public SharkAggressionState AggressiveState()
        {
            return _states[SharkAggressionStates.Aggressive];
        }
        public SharkAggressionState PassiveState()
        {
            return _states[SharkAggressionStates.Passive];
        }
        public SharkAggressionState StunnedState()
        {
            return _states[SharkAggressionStates.Stunned];
        }
        public SharkAggressionState CooldownState()
        {
            return _states[SharkAggressionStates.Cooldown];
        }
    }
}
