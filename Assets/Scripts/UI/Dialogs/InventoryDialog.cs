using DataModels;
using UnityEngine;
using Zenject;

namespace UI.Dialogs
{
    public class InventoryDialog : ClosableDialog
    {
        [SerializeField] private RectTransform _grid;
        
        private Inventory _inventory;
        private InventoryItemsContainer _inventoryItemsContainer;
        
        [Inject]
        private void InjectDependencies(InventoryItemsContainer inventoryItemsContainer)
        {
            _inventoryItemsContainer = inventoryItemsContainer;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Time.timeScale = 0.0f;
        }

        public void SetModel(Inventory inventory)
        {
            _inventory = inventory;
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            foreach (var pair in _inventory)
            {
                Debug.Log("Item: " + pair);
            }
        }

        protected override void OnDisable()
        {
            Time.timeScale = 1.0f;
            base.OnDisable();
        }
    }
}