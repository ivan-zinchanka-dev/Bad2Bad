using DataModels;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Collider2D))]
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField] private int _count = 1;
        
        public InventoryItem Item => _inventoryItem;
        public int Count => _count;
    }
}