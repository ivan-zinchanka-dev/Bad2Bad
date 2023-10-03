using System;
using UnityEngine;

namespace DataModels
{
    [CreateAssetMenu(fileName = "inventory_item", menuName = "DataModels/InventoryItem", order = 0)]
    public class InventoryItem : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
    }
}