using FoGD.Shark.States.Bank;

namespace FoGD.Shark.States
{
    public abstract class SharkAggressionState
    { 
        protected SharkBehaviour Context
        { get; private set; }
        protected SharkAggressionStateBank Bank
        { get; private set; }

        protected SharkAggressionState(SharkBehaviour context, SharkAggressionStateBank bank)
        {
            Context = context;
            Bank = bank;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();

        protected virtual void CheckSwitchStates() 
        { }

        protected void SwitchState(SharkAggressionState newState)
        {
            ExitState();

            newState.EnterState();
            Context.CurrentState = newState;
        }
    }
}
