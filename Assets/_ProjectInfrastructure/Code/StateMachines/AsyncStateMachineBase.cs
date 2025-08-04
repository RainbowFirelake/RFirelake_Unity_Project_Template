using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace RFirelake.Infrastructure.StateMachines
{
    public abstract class AsyncStateMachineBase
    {
        public AsyncStateBase CurrentState { get; protected set; }

        private Dictionary<Type, AsyncStateBase> _states;

        public void AddState(Type stateType, AsyncStateBase asyncState) => _states[stateType] = asyncState;
        public async UniTask EnterState<T>() where T : AsyncStateBase
        {
            CurrentState?.OnExitAsync();
            CurrentState = _states[typeof(T)];
            await CurrentState.OnEnterAsync();
        }
    }

    public abstract class AsyncStateBase
    {
#pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
        public virtual async UniTask OnEnterAsync() { }
        public virtual async UniTask OnExitAsync() { }
#pragma warning restore CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
    }
}
