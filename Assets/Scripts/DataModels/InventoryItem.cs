using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DataModels
{
    [CreateAssetMenu(fileName = "inventory_item", menuName = "DataModels/InventoryItem", order = 0)]
    public class InventoryItem : ScriptableObject
    {
        [field: SerializeField] public string NameKey { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}