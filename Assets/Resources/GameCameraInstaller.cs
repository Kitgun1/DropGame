using UnityEngine;
using Zenject;

namespace Resources
{
    public class GameCameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle().NonLazy();
        }
    }
}