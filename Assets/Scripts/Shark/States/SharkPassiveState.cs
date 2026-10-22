using UnityEngine;
using FoGD.Shark.States.Bank;

namespace FoGD.Shark.States
{
    public class SharkPassiveState : SharkAggressionState
    {
        private float _timer;

        public SharkPassiveState(SharkBehaviour context, SharkAggressionStateBank bank) : base(context, bank)
        { }

        public override void EnterState()
        {
            // Get the correct lerp position if the position is in range
            _timer = (Context.Position.x - Context.XPosRange.x * Context.Direction) / (Context.XPosRange.y * Context.Direction - Context.XPosRange.x * Context.Direction);

            if (_timer < 0f)
                _timer = 0f;
        }
        public override void UpdateState()
        {
            CheckSwitchStates();

            // Calculate x position at the current time
            float x = Mathf.Lerp(Context.XPosRange.x * Context.Direction, Context.XPosRange.y * Context.Direction, _timer);

            Context.Position = new Vector2(x, Context.transform.position.y);

            // Advance time
            _timer += Time.deltaTime / Context.SwimSpeed;
            _timer = Mathf.Clamp01(_timer);

            if (_timer == 1f)
            {
                Context.Flip();
                _timer = 0f;
            }
        }
        public override void ExitState()
        { }

        protected override void CheckSwitchStates()
        {
            // Calculate aggression
            Context.Aggression = Mathf.PerlinNoise1D(Time.time + 150f);

            if (Context.Aggression <= Context.AggroToJump)
            {
                SwitchState(Bank.AggressiveState());
            }
        }
    }
}
