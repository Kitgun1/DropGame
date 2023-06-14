using _Project.Sound;
using UnityEngine;
using Zenject;

namespace Resources
{
    public class SoundInstaller : MonoInstaller
    {
        [SerializeField] private Sound _sound;

        public override void InstallBindings()
        {
            Container.Bind<Sound>().FromInstance(_sound).AsSingle().NonLazy();
        }
    }
}