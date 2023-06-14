using System;
using System.Collections.Generic;

namespace _Project.FSM
{
    public class FSM
    {
        private FSMState StateCurrent { get; set; }
        private Dictionary<Type, FSMState> _states = new Dictionary<Type, FSMState>();

        public void AddState(FSMState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<TState>() where TState : FSMState
        {
            Type type = typeof(TState);

            if (StateCurrent != null && StateCurrent.GetType() == type)
            {
                return;
            }

            if (!_states.TryGetValue(type, out FSMState newState)) return;

            StateCurrent?.Exit();
            StateCurrent = newState;
            StateCurrent.Enter();
        }

        public void Update()
        {
            StateCurrent?.Update();
        }

        public void FixedUpdate()
        {
            StateCurrent?.FixedUpdate();
        }
    }
}