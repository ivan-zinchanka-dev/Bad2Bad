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
        
        private InventoryItem _inventoryItem;
        private int _count;

        public InventoryItemView SetModel(InventoryItem inventoryItem, int count)
        {
            _inventoryItem = inventoryItem;
            _count = count;
            UpdateView();
            return this;
        }

        private void UpdateView()
        {
            if (_inventoryItem == null) 
                return;

            Debug.Log("Set");
            
            _icon.sprite = _inventoryItem.Icon;
            _countTextMesh.SetText(_count > 0 ? _count.ToString() : string.Empty);
        }
    }
}