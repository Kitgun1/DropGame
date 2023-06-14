using Zenject;

namespace _Project.FSM
{
    public abstract class FSMState
    {
        protected readonly FSM Fsm;

        protected FSMState(FSM fsm)
        {
            Fsm = fsm;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
        protected virtual void InitListener() { }
        protected virtual void RemoveListener() { }
    }
}