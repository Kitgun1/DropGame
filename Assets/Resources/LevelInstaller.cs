using _Project.Level;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private Level _level;
    [SerializeField] private Transform _parent;

    public override void InstallBindings()
    {
        Level levelInstance =
                Container.InstantiatePrefabForComponent<Level>(_level, Vector3.zero, Quaternion.identity, _parent);
        levelInstance.name = _level.name;
        Container.Bind<Level>().FromInstance(levelInstance).AsSingle().NonLazy();
    }
}