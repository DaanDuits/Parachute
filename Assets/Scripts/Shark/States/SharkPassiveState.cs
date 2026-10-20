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
        { }
        public override void UpdateState()
        {
            float x = Mathf.Lerp(Context.XPosRange.x * Context.Direction, Context.XPosRange.y * Context.Direction, _timer);

            Context.Position = new Vector2(x, Context.transform.position.y);

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
    }
}
