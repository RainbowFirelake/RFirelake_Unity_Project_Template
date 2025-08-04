using System;
using System.Collections.Generic;

namespace RFirelake.Infrastructure.StateMachines
{
    public abstract class StateMachineBase
    {
        public StateBase CurrentState { get; protected set; }
        private Dictionary<Type, StateBase> _states;

        public void AddState(Type stateType, StateBase state) => _states[stateType] = state;
        public void EnterState<T>() where T : StateBase
        {
            CurrentState?.OnExit();
            CurrentState = _states[typeof(T)];
            CurrentState.OnEnter();
        }

        public void Update()
        {
            CurrentState?.OnUpdate();
        }
    }

    public abstract class StateBase
    {
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
}
