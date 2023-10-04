using System;
using DataModels;
using UI.Views;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace UI.Dialogs
{
    public class InventoryDialog : ClosableDialog
    {
        [SerializeField] private InventoryItemView _itemViewPrefab;
        [SerializeField] private RectTransform _gridLayout;
        
        private Inventory _inventory;
        private InventoryItemsContainer _inventoryItemsContainer;
        
        [Inject]
        private void InjectDependencies(InventoryItemsContainer inventoryItemsContainer)
        {
            _inventoryItemsContainer = inventoryItemsContainer;
        }

        /*private void Awake()
        {
            _inventoryItemsContainer = StaticContext.Container.Resolve<InventoryItemsContainer>();
        }*/

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

                InventoryItem item = _inventoryItemsContainer.GetItemByKey(pair.Key);
                Instantiate(_itemViewPrefab, _gridLayout, false).SetModel(item, pair.Value);
            }
        }

        protected override void OnDisable()
        {
            Time.timeScale = 1.0f;
            base.OnDisable();
        }
    }
}