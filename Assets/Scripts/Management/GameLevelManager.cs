using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Management
{
    public class GameLevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private List<CollectableItem> _collectableItems;

        public GameObject Player => _player;
    }
}