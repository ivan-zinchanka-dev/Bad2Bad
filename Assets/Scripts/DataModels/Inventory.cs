using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataModels
{
    [Serializable]
    public class Inventory
    {
        private Dictionary<string, int> _counts = new Dictionary<string, int>();

        public void AddItem(string itemName, int count = 1)
        {
            if (!CountArgumentCheck(count))
            {
                return;
            }
            
            if (_counts.ContainsKey(itemName))
            {
                _counts[itemName] += count;
            }
            else
            {
                _counts.Add(itemName, count);
            }
        }
        
        public void RemoveItem(string itemName, int count = 1)
        {
            if (!CountArgumentCheck(count))
            {
                return;
            }

            if (_counts.ContainsKey(itemName))
            {
                _counts[itemName] -= count;

                if (_counts[itemName] <= 0)
                {
                    _counts.Remove(itemName);
                }
            }
        }

        private static bool CountArgumentCheck(int count)
        {
            if (count < 0)
            {
                Debug.LogException(new ArgumentOutOfRangeException(nameof(count), count, "should be positive"));
                return false;
            }
            else 
                return true;
        }

    }
}