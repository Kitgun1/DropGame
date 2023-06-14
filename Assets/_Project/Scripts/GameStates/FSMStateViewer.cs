using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.States
{
    public class FSMStateViewer : FSMStateGame
    {
        private readonly PlayerInput _input;

        public FSMStateViewer(FSM.FSM fsm, PlayerInput input) : base(fsm)
        {
            _input = input;
        }

        public override void Enter()
        {
            InitListener();
        }

        protected override void InitListener()
        {
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void Exit()
        {
            RemoveListener();
        }

        protected override void RemoveListener()
        {
        }
    }
}