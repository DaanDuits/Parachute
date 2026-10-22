using UnityEngine;
using FoGD.Shark.States.Bank;

namespace FoGD.Shark.States
{
    public class SharkAggressiveState : SharkAggressionState
    {
        private float _startX;
        private float _targetX;

        private float _timer = 0f;

        public SharkAggressiveState(SharkBehaviour context, SharkAggressionStateBank bank) : base(context, bank)
        { }

        public override void EnterState()
        {
            // Get the start and target possitions
            _startX = Context.Position.x;
            _targetX = GameManager.Instance.PlayerPosition.x;

            // Flip if needed
            int jumpDir = _startX > _targetX ? -1 : 1;
            if (jumpDir != Context.Direction)
                Context.Flip();
        }
        public override void UpdateState()
        {
            // Calculate new jump position
            float x = Mathf.Lerp(_startX, _targetX, _timer);
            float y = 4f * (Context.YPosRange.y - Context.YPosRange.x) * _timer * (1f - _timer) + Context.YPosRange.x;

            // Calculate rotation in jump
            Vector2 diff;
            if (_startX > _targetX)
                diff = Context.Position - new Vector2(x, y);
            else
                diff = new Vector2(x, y) - Context.Position;

            float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Context.Rotation = Quaternion.Euler(0f, 0f, rotation);

            // Set new position
            Context.Position = new Vector2(x, y);

            // Advance time
            _timer += Time.deltaTime / Context.JumpSpeed;
            _timer = Mathf.Clamp01(_timer);

            if (_timer >= 0.5f && !Context.IsCollider)
            {
                Context.EnableCollider(true);
            }

            if (_timer == 1f)
            {
                SwitchState(Bank.PassiveState());
                _timer = 0f;
            }
        }
        public override void ExitState()
        {
            // Reset rotation
            Context.Rotation = Quaternion.identity;
            Context.EnableCollider(false);
        }
    }
}
