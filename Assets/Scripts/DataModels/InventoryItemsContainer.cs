using System.Collections.Generic;
using UnityEngine;

namespace DataModels
{
    [CreateAssetMenu(fileName = "inventory_items_container", menuName = "DataModels/InventoryItemsContainer", order = 0)]
    public class InventoryItemsContainer : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> _inventoryItems;

        public InventoryItem GetItemByKey(string nameKey)
        {
            return _inventoryItems.Find(item => item.NameKey == nameKey);
        }

    }
}