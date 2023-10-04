using DataModels;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ContainersInstaller : MonoInstaller
    {
        [SerializeField] private InventoryItemsContainer _inventoryItemsContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<InventoryItemsContainer>().FromScriptableObject(_inventoryItemsContainer).AsSingle().NonLazy();
        }
    }
}