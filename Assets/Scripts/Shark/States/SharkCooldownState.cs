using UnityEngine;
using FoGD.Shark.States.Bank;

namespace FoGD.Shark.States
{
    internal class SharkCooldownState : SharkAggressionState
    {
        private float _swimTimer, _cooldownTimer;
        private float _cooldownTime;

        public SharkCooldownState(SharkBehaviour context, SharkAggressionStateBank bank) : base(context, bank)
        { }

        public override void EnterState()
        {
            // Get the correct lerp position if the position is in range
            _swimTimer = (Context.Position.x - Context.XPosRange.x * Context.Direction) / (Context.XPosRange.y * Context.Direction - Context.XPosRange.x * Context.Direction);

            // Setup cooldown
            _cooldownTime = Random.value * Context.JumpCooldown;
            _cooldownTimer = 0f;

            if (_swimTimer < 0f)
                _swimTimer = 0f;
        }
        public override void UpdateState()
        {
            CheckSwitchStates();

            // Calculate x position at the current time
            float x = Mathf.Lerp(Context.XPosRange.x * Context.Direction, Context.XPosRange.y * Context.Direction, _swimTimer);

            Context.Position = new Vector2(x, Context.transform.position.y);

            // Advance time
            _swimTimer += Time.deltaTime / Context.SwimSpeed;
            _swimTimer = Mathf.Clamp01(_swimTimer);
            _cooldownTimer += Time.deltaTime;

            if (_swimTimer == 1f)
            {
                Context.Flip();
                _swimTimer = 0f;
            }
        }
        public override void ExitState()
        { }

        protected override void CheckSwitchStates()
        {
            if (_cooldownTimer / _cooldownTime >= 1f)
                SwitchState(Bank.AggressiveState());
        }
    }
}
