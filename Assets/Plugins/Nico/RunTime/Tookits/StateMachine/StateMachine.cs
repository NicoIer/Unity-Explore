using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nico
{
    [Serializable]
    public class StateMachine<TOwner> // where TOwner : MonoBehaviour
    {
        public TOwner Owner { get; private set; }
        [field: SerializeReference] public State<TOwner> currentState { get; protected set; }
        private Dictionary<Type, State<TOwner>> _stateDic = new Dictionary<Type, State<TOwner>>();

        public StateMachine(TOwner owner)
        {
            Owner = owner;
        }

        public bool Get<TState>(out TState state) where TState : State<TOwner>
        {
            if (_stateDic.TryGetValue(typeof(TState), out var value))
            {
                state = (TState)value;
                return true;
            }

            state = null;
            return false;
        }

        public void Start<T>() where T : State<TOwner>
        {
            currentState = _stateDic[typeof(T)];
            currentState.OnEnter();
        }

        public virtual void Change<T>() where T : State<TOwner>
        {
            currentState.OnExit();
            currentState = _stateDic[typeof(T)];
            currentState.OnEnter();
        }

        public void Add<T>(T state) where T : State<TOwner>
        {
            if (_stateDic.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"state:{typeof(T)} is already in state machine");
                return;
            }

            _stateDic.Add(typeof(T), state);
            state.SetStateMachine(this);
        }

        public virtual void OnUpdate()
        {
            currentState.OnUpdate();
        }
    }
}