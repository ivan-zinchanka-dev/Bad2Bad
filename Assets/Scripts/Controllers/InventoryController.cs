using DataModels;
using Factories;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class InventoryController : MonoBehaviour
    {
        private Inventory _inventory;
        private DialogsFactory _dialogsFactory;
        
        [Inject]
        private void InjectDependencies(DialogsFactory dialogsFactory)
        {
            _dialogsFactory = dialogsFactory;
        }

        private void Start()
        {
            _inventory = new Inventory();
        }

        public void AddItem(InventoryItem item, int count)
        {
            _inventory.AddItem(item.NameKey, count);
        }

        public void RemoveItem(InventoryItem item, int count)
        {
            _inventory.RemoveItem(item.NameKey, count);
        }
        
        public void OpenDialog()
        {
            _dialogsFactory.ShowInventoryDialog().SetModel(_inventory);
        }

        [EasyButtons.Button]
        public void ClearInventory()
        {
            _inventory.Clear();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<CollectableItem>(out CollectableItem collectableItem))
            {
                AddItem(collectableItem.Item, collectableItem.Count);
                collectableItem.Collect();
            }
        }
    }
}