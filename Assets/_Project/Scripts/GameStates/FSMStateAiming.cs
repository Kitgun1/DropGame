using System;
using _Project.Units.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace _Project.States
{
    public sealed class FSMStateAiming : FSMStateGame
    {
        private readonly PlayerInput _input;
        private readonly Canvas _canvas;
        private readonly LineRenderer _trajectory;
        private readonly Camera _camera;
        private readonly Transform _transform;

        private Vector2 _direction;
        private bool _availableAiming;

        public FSMStateAiming(FSM.FSM fsm, Transform transform, PlayerInput input, Canvas canvas, Camera camera,
            LineRenderer trajectory) : base(fsm)
        {
            _input = input;
            _canvas = canvas;
            _trajectory = trajectory;
            _camera = camera;
            _transform = transform;
        }

        public override void Enter()
        {
            _trajectory.gameObject.SetActive(false);
            InitListener();
        }

        protected override void InitListener()
        {
            _input.Mouse.LBCDelayed.performed += TryEnableAiming;
            _input.Mouse.LBCDelayed.canceled += TryFire;
        }

        public override void Update()
        {
            if (!_availableAiming) return;
            Aiming(1.5f);
        }

        public override void Exit()
        {
            RemoveListener();
        }

        protected override void RemoveListener()
        {
            _input.Mouse.LBCDelayed.performed -= TryEnableAiming;
            _input.Mouse.LBCDelayed.canceled -= TryFire;
        }


        private void TryFire(InputAction.CallbackContext ctx)
        {
            if (GetDifference() <= 20 || _availableAiming == false)
            {
                _trajectory.gameObject.SetActive(false);
                return;
            }

            {
                var ball = Object.Instantiate(new GameObject("Ball"), _transform.position, Quaternion.identity,
                    _transform);
                ball.AddComponent<SpriteRenderer>().sprite = UnityEngine.Resources.Load<Sprite>("circle");
                ball.AddComponent<CircleCollider2D>();
                ball.AddComponent<Rigidbody2D>().AddForce(_direction, ForceMode2D.Impulse);
                ball.AddComponent<Ball>().SetDamage(1);
                ball.transform.localScale = Vector3.one * 0.5f;
            }

            _availableAiming = false;
            _trajectory.gameObject.SetActive(false);
        }

        private void TryEnableAiming(InputAction.CallbackContext ctx)
        {
            if (GetDifference() <= 5)
            {
                _availableAiming = false;
                return;
            }

            _availableAiming = true;
        }

        private void Aiming(float maxDistance = 10f)
        {
            Vector3 worldMouse = _camera.ScreenToWorldPoint(Mouse.current.position.value);
            Vector3 position = _transform.position;
            Vector3 direction = worldMouse - position;
            _direction = direction.normalized * maxDistance * 5;
            Vector3 clampedPosition = position + Vector3.ClampMagnitude(direction, maxDistance);

            _trajectory.SetPosition(0, position);
            _trajectory.SetPosition(1, clampedPosition);
            _trajectory.gameObject.SetActive(true);
        }

        private float GetDifference()
        {
            return Vector2.Distance(Mouse.current.position.value, _camera.WorldToScreenPoint(_transform.position)) /
                   _canvas.scaleFactor;
        }
    }
}