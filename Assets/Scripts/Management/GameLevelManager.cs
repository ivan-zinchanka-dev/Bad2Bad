using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Zenject;

namespace Management
{
    public class GameLevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        
        [SerializeField] private GameObject _player;
        [SerializeField] private List<CollectableItem> _collectableItems;

        [SerializeField] private Rect _enemySpawnField;

        private DiContainer _diContainer;
        
        public GameObject Player => _player;

        [Inject]
        private void InjectDependencies(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void SpawnEnemies()
        {
            
            
        }


    }
}