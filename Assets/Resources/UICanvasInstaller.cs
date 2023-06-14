using UnityEngine;
using Zenject;

public sealed class UICanvasInstaller : MonoInstaller
{
    [SerializeField] private Canvas _uiCanvas; 
    
    public override void InstallBindings()
    {
        Container.Bind<Canvas>().FromInstance(_uiCanvas).AsSingle();
    }
}