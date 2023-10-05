using DataModels;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Collider2D))]
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField] private int _count = 1;

        [Space]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public InventoryItem Item => _inventoryItem;
        public int Count => _count;

        public CollectableItem SetModel(InventoryItem inventoryItem, int count = 1)
        {
            _inventoryItem = inventoryItem;
            _count = count;
            _spriteRenderer.sprite = _inventoryItem.Icon;
            return this;
        }

        public void Collect()
        {
            Destroy(gameObject);
        }
    }
}