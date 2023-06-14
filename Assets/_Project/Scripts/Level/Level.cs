using System.Collections.Generic;
using _Project.Units.Player;
using UnityEngine;
using Zenject;

namespace _Project.Level
{
    public sealed class Level : MonoBehaviour
    {
        [SerializeField] private List<LevelTemplate> _levels = new List<LevelTemplate>();

        [Inject] private Player _player;
        private LevelTemplate _spawnedLevel;

        private void Start()
        {
            ConstructLevel();
        }

        private void ConstructLevel()
        {
            ClearLevel();
            int index = Random.Range(0, _levels.Count);
            _spawnedLevel = Instantiate(_levels[index])
                .InitializeItemResources()
                .InitializeSpawnerPoints(_player);
            _spawnedLevel.name = _levels[index].name;
            
            _player.SetRandomPoint();
        }

        private void ClearLevel()
        {
            if (_spawnedLevel == null) return;

            Destroy(_spawnedLevel.gameObject);
        }
    }
}