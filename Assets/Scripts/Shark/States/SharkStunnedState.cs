using FoGD.Shark.States.Bank;
using UnityEngine;

namespace FoGD.Shark.States
{
    public class SharkStunnedState : SharkAggressionState
    {
        private float _timer;
        private float _stunTime;

        public SharkStunnedState(SharkBehaviour context, SharkAggressionStateBank bank) : base(context, bank)
        { }

        public override void EnterState()
        {
            _timer = 0f;
            _stunTime = Context.StunTime;
            Context.StunTime = 0f;
            Context.IsStunned = false;
        }
        public override void UpdateState()
        {
            _timer += Time.deltaTime / _stunTime;
            _timer = Mathf.Clamp01(_timer);

            if (_timer == 1f)
                SwitchState(Bank.PassiveState());
        }
        public override void ExitState()
        { }
    }
}
