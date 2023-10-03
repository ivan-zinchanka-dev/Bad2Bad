using Factories;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private ProjectilesFactory _projectilesFactory;
        [SerializeField] private DialogsFactory _dialogsFactory;
        
        public override void InstallBindings()
        {
            Container.Bind<ProjectilesFactory>().FromInstance(_projectilesFactory).AsSingle().NonLazy();
            Container.Bind<DialogsFactory>().FromInstance(_dialogsFactory).AsSingle().NonLazy();
        }
    }
}