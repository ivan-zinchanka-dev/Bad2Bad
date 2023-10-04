using UI.Dialogs;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class DialogsFactory : MonoBehaviour
    {
        [SerializeField] private Canvas _dialogCanvas;
        
        [Space]
        [SerializeField] private InventoryDialog _inventoryDialogPrefab;
        //[] other dialogs

        private DiContainer _diContainer;
        
        [Inject]
        private void InjectDependencies(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public InventoryDialog ShowInventoryDialog()
        {
            return _diContainer.InstantiatePrefabForComponent<InventoryDialog>(_inventoryDialogPrefab, _dialogCanvas.transform);
            
            //return Instantiate<InventoryDialog>(_inventoryDialogPrefab, _dialogCanvas.transform, false);
        }
    }
}