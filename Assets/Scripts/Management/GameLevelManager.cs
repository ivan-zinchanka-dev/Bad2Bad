using System.Collections.Generic;
using Controllers;
using DataModels;
using Factories;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Management
{
    public class GameLevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        
        [SerializeField] private GameObject _player;
        [SerializeField] private List<InventoryItem> _loot;

        [SerializeField] private Rect _lootSpawnField;
        [SerializeField] private Rect _enemySpawnField;
        
        private DiContainer _diContainer;
        private CollectablesFactory _collectablesFactory;
        private DialogsFactory _dialogsFactory;
        
        public GameObject Player => _player;

        [Inject]
        private void InjectDependencies(DiContainer diContainer, CollectablesFactory collectablesFactory, DialogsFactory dialogsFactory)
        {
            _diContainer = diContainer;
            _collectablesFactory = collectablesFactory;
            _dialogsFactory = dialogsFactory;
        }
        
        private T GetRandomItem<T>(List<T> collection)
        {
            return collection[Random.Range(0, collection.Count)];
        }
        
        private Vector2 GetRandomPosition(Rect field)
        {
            return new Vector2(
                Random.Range(field.xMin, field.xMax),
                Random.Range(field.yMin, field.yMax));
        }
        
        private void SpawnLoot()
        {
            for (int i = 0; i < 5; i++)
            {
                _collectablesFactory.CreateCollectableItem(GetRandomItem<InventoryItem>(_loot),
                    GetRandomPosition(_lootSpawnField));
            }
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < 3; i++)
            {
                _diContainer.InstantiatePrefab(_enemyPrefab, 
                    GetRandomPosition(_enemySpawnField), Quaternion.identity, null);
            }
        }

        private void Start()
        {
            SpawnLoot();
            SpawnEnemies();

            _player.GetComponent<HealthBody>().OnDeath.AddListener(Defeat);
        }

        private void Defeat()
        {
            Time.timeScale = 0.0f;
            _dialogsFactory.ShowDefeatDialog();
        }
    }
}