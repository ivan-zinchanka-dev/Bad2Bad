using System;
using DataModels;
using UnityEngine;

namespace Controllers
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;

        public void AddItem(InventoryItem item, int count)
        {
            _inventory.AddItem(item.name, count);
        }

        public void RemoveItem(InventoryItem item, int count)
        {
            _inventory.RemoveItem(item.name, count);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<InventoryItemView>(out InventoryItemView itemView))
            {
                AddItem(itemView.Item, itemView.Count); ;
            }
        }

        public void OpenDialog()
        {
            
            
        }
    }
}