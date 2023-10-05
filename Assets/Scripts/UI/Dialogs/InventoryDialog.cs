using System.Collections.Generic;
using DataModels;
using UI.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Dialogs
{
    public class InventoryDialog : ClosableDialog
    {
        [SerializeField] private InventoryItemView _itemViewPrefab;
        [SerializeField] private RectTransform _gridLayout;
        [SerializeField] private Button _removeItemButton;
        
        private Inventory _inventory;
        private InventoryItemsContainer _inventoryItemsContainer;
        
        private readonly Dictionary<InventoryItem, InventoryItemView> _itemModelViews = 
            new Dictionary<InventoryItem, InventoryItemView>();
        
        [Inject]
        private void InjectDependencies(InventoryItemsContainer inventoryItemsContainer)
        {
            _inventoryItemsContainer = inventoryItemsContainer;
        }

        private void Awake()
        {
            _removeItemButton.gameObject.SetActive(false);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _removeItemButton.onClick.AddListener(RemoveSelectedItem);
            InventoryItemView.OnItemSelected += OnItemSelected;
            Time.timeScale = 0.0f;
        }

        public void SetModel(Inventory inventory)
        {
            _inventory = inventory;
            CrateGrid();
        }

        private void CrateGrid()
        {
            _itemModelViews.Clear();
            
            foreach (var pair in _inventory)
            {
                Debug.Log("Item: " + pair);

                InventoryItem itemModel = _inventoryItemsContainer.GetItemByKey(pair.Key);
                InventoryItemView itemView = Instantiate<InventoryItemView>(_itemViewPrefab, _gridLayout, false)
                    .SetModel(itemModel, pair.Value);
                
                _itemModelViews.Add(itemModel, itemView);
            }
        }
        
        private void OnItemSelected(InventoryItemView selectedItemView)
        {
            _removeItemButton.gameObject.SetActive(true);
        }

        private void RemoveSelectedItem()
        {
            InventoryItemView selectedItemView = InventoryItemView.SelectedItemView;

            if (selectedItemView != default)
            {
                InventoryItem selectedItemModel = selectedItemView.Model;
                
                _inventory.RemoveItem(selectedItemModel.NameKey);
                
                if (!_itemModelViews.ContainsKey(selectedItemModel))
                    return;
                
                InventoryItemView itemView = _itemModelViews[selectedItemModel];
                
                if (_inventory.GetCountByNameKey(selectedItemModel.NameKey) > 0)
                {
                    itemView.UpdateCount(_inventory.GetCountByNameKey(selectedItemModel.NameKey));
                }
                else
                {
                    _itemModelViews.Remove(selectedItemModel);
                    Destroy(itemView.gameObject);
                }
            }
        }

        protected override void OnDisable()
        {
            Time.timeScale = 1.0f;
            InventoryItemView.OnItemSelected -= OnItemSelected;
            _removeItemButton.onClick.RemoveListener(RemoveSelectedItem);
            base.OnDisable();
        }
    }
}