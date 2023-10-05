using Controllers;
using DataModels;
using UnityEngine;

namespace Factories
{
    public class CollectablesFactory : MonoBehaviour
    {
        [SerializeField] private CollectableItem _collectableItemPrefab;

        public CollectableItem CreateCollectableItem(InventoryItem itemModel, Vector3 position)
        {
            return Instantiate<CollectableItem>(_collectableItemPrefab, position, Quaternion.identity).SetModel(itemModel);
        }
    }
}