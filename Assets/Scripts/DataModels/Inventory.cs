using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace DataModels
{
    public class Inventory : IEnumerable<KeyValuePair<string, int>>
    {
        private const string DatabaseKey = "inventory_";
        private Dictionary<string, int> _counts;

        public Inventory()
        {
            _counts = DataUtility.Get<Dictionary<string, int>>(DatabaseKey) ?? new Dictionary<string, int>();
        }
        
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
            
            DataUtility.Save(DatabaseKey, _counts);
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
                
                DataUtility.Save(DatabaseKey, _counts);
            }
        }
        
        public int GetCountByNameKey(string nameKey)
        {
            if (_counts.TryGetValue(nameKey, out int count))
            {
                return count;
            }

            return 0;
        }
        
        public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
        {
            foreach (var pair in _counts)
            {
                yield return pair;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _counts.Clear();
            DataUtility.Clear(DatabaseKey);
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