using System.Collections.Generic;
using System.Linq;
using _Project.Level.Spawner;
using _Project.States;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Units.Player
{
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private LineRenderer _trajectory;

        [Inject] private PlayerInput _input;
        [Inject] private Canvas _canvas;
        [Inject] private Camera _camera;
        private List<SpawnerPoint> _points;
        private SpawnerPoint _currentPoint;
        private FSM.FSM _fsm;

        private void Start()
        {
            _fsm = new FSM.FSM();
            _fsm.AddState(new FSMStateAiming(_fsm, transform, _input, _canvas, _camera, _trajectory));
            _fsm.SetState<FSMStateAiming>();
        }

        private void Update()
        {
            _fsm.Update();
        }

        private void FixedUpdate()
        {
            _fsm.FixedUpdate();
        }

        public void InitializePoints(IEnumerable<SpawnerPoint> points)
        {
            _points = points.ToList();
        }

        public void SetRandomPoint()
        {
            int randIndex = Random.Range(0, _points.Count);
            _currentPoint = _points[randIndex];

            transform.position = _currentPoint.transform.position;
        }
    }
}