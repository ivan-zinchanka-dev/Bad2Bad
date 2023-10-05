using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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

        private Vector2 GetRandomPosition()
        {
            return new Vector2(
                Random.Range(_enemySpawnField.xMin, _enemySpawnField.xMax),
                Random.Range(_enemySpawnField.yMin, _enemySpawnField.yMax));
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < 3; i++)
            {
                _diContainer.InstantiatePrefab(_enemyPrefab, GetRandomPosition(), Quaternion.identity, null);
            }
        }

        private void Start()
        {
            SpawnEnemies();
        }
    }
}