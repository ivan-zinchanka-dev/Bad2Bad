using System;
using System.Collections.Generic;
using DataModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _countTextMesh;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _selectedBackground;
        
        private InventoryItem _inventoryItem;
        private int _count;

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            
            private set
            {
                if (value)
                {
                    foreach (InventoryItemView itemView in AllItemViews)
                    {
                        itemView.IsSelected = false;
                    }
                }

                _isSelected = value;
                _selectedBackground.enabled = _isSelected;

                if (_isSelected)
                {
                    OnItemSelected?.Invoke(this);
                }
                else
                {
                    OnItemDeselected?.Invoke(this);
                }
            }
        }

        
        
        private static readonly List<InventoryItemView> AllItemViews = new List<InventoryItemView>();
        public static InventoryItemView SelectedItemView => AllItemViews.Find(itemView => itemView.IsSelected);

        public static event Action<InventoryItemView> OnItemSelected;
        public static event Action<InventoryItemView> OnItemDeselected;
        
        public InventoryItem Model => _inventoryItem;
        
        private void Awake()
        {
            AllItemViews.Add(this);
        }
        
        private void OnEnable()
        {
            _selectButton.onClick.AddListener(Select);
        }

        public InventoryItemView SetModel(InventoryItem inventoryItem, int count)
        {
            _inventoryItem = inventoryItem;
            _count = count;
            UpdateView();
            return this;
        }

        public void UpdateCount(int count)
        {
            _count = count;
            UpdateView();
        }

        public void Select()
        {
            IsSelected = true;
        }
        
        private void UpdateView()
        {
            if (_inventoryItem == null) 
                return;
            
            _icon.sprite = _inventoryItem.Icon;
            _countTextMesh.SetText(_count > 0 ? _count.ToString() : string.Empty);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(Select);
        }

        private void OnDestroy()
        {
            AllItemViews.Remove(this);
        }
    }
}