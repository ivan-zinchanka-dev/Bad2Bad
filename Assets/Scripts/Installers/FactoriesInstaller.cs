using Factories;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private ProjectilesFactory _projectilesFactory;
        
        public override void InstallBindings()
        {
            Container.Bind<ProjectilesFactory>().FromInstance(_projectilesFactory).AsSingle();
            
        }
    }
}