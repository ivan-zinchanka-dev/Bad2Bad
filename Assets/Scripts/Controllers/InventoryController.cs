using System;
using DataModels;
using Factories;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;

        private DialogsFactory _dialogsFactory;
        
        [Inject]
        private void InjectDependencies(DialogsFactory dialogsFactory)
        {
            _dialogsFactory = dialogsFactory;
        }

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
            if (other.TryGetComponent<CollectableItem>(out CollectableItem collectableItem))
            {
                AddItem(collectableItem.Item, collectableItem.Count);
                collectableItem.Collect();
            }
        }

        public void OpenDialog()
        {
            _dialogsFactory.ShowInventoryDialog();
        }
    }
}