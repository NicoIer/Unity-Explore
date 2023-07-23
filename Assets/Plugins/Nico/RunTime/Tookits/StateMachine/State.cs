﻿namespace Nico
{
    public abstract class State<T>
    {
        public T owner { get; private set; }
        protected StateMachine<T> stateMachine;

        public State(T owner)
        {
            this.owner = owner;
        }
        internal void SetStateMachine(StateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}