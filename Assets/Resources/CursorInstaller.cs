using _Project.Inventory.PlayerCursor;
using UnityEngine;
using Zenject;

public class CursorInstaller : MonoInstaller
{
    [SerializeField] private PlayerCursor _playerCursor;

    public override void InstallBindings()
    {
        Container.Bind<PlayerCursor>().FromInstance(_playerCursor).AsSingle().NonLazy();
    }
}