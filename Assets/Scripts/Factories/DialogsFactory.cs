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
        [SerializeField] private GameObject _defeatDialogPrefab;

        private DiContainer _diContainer;
        
        [Inject]
        private void InjectDependencies(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public InventoryDialog ShowInventoryDialog()
        {
            return _diContainer.InstantiatePrefabForComponent<InventoryDialog>(_inventoryDialogPrefab, _dialogCanvas.transform);
        }
        
        public void ShowDefeatDialog()
        {
            _diContainer.InstantiatePrefab(_defeatDialogPrefab, _dialogCanvas.transform);
        }
    }
}