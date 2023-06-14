using System.Collections.Generic;
using _Project.Inventory.ScriptableObjects;
using _Project.Level.Resource;
using _Project.Level.Spawner;
using _Project.Units.Player;
using UnityEngine;

namespace _Project.Level
{
    public class LevelTemplate : MonoBehaviour
    {
        [SerializeField] private ComplexityType _complexity;
        [SerializeField] private List<ItemSO> _allowedItems;
        [SerializeField] private List<SpawnerPoint> _spawnerPoints = new List<SpawnerPoint>();
        [SerializeField] private List<ResourcePoint> _resources = new List<ResourcePoint>();
        
        public ComplexityType Complexity
        {
            get => _complexity;
            private set => _complexity = value;
        }

        #region Resources

        public LevelTemplate InitializeItemResources()
        {
            foreach (ResourcePoint resource in _resources)
            {
                int randIndex = Random.Range(0, _allowedItems.Count);
                resource.Initialize(_allowedItems[randIndex]);
            }

            return this;
        }

        #endregion

        #region Spawners

        public LevelTemplate InitializeSpawnerPoints(Player player)
        {
            player.InitializePoints(_spawnerPoints);
            return this;
        }

        #endregion

        #region Gates

        #endregion
    }
}