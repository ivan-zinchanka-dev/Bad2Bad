using Management;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ManagementInstaller : MonoInstaller
    {
        [SerializeField] private GameLevelManager _gameLevelManager;
        
        public override void InstallBindings()
        {
            Container.Bind<GameLevelManager>().FromInstance(_gameLevelManager).AsSingle().NonLazy();
        }
    }
}