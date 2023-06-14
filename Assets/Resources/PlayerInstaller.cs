using _Project.Units.Player;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerSpawnPoint;

    public override void InstallBindings()
    {
         Player playerInstance = Container.InstantiatePrefabForComponent<Player>(_player,
            _playerSpawnPoint.position, _playerSpawnPoint.rotation, _playerSpawnPoint);
         playerInstance.name = _player.name;

        Container.Bind<Player>().FromInstance(playerInstance).AsSingle().NonLazy();
    }
}