using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Pool;

namespace Factories
{
    public class ProjectilesFactory : MonoBehaviour
    {
        [SerializeField] private List<ProjectilePoolData> _poolsData;
        
        [Serializable]
        public struct ProjectilePoolData
        {
            [field:SerializeField] public ProjectileType ProjectileType { get; private set; }
            [field:SerializeField] public Projectile ProjectilePrefab { get; private set; }
            [field:SerializeField] public int InitialPoolCapacity { get; private set; }
        }
        
        private Dictionary<ProjectileType, IObjectPool<Projectile>> _pools;

        private void Awake()
        {
            _pools = new Dictionary<ProjectileType, IObjectPool<Projectile>>(_poolsData.Count);
            
            foreach (ProjectilePoolData data in _poolsData)
            {
                _pools.Add(data.ProjectileType, new ObjectPool<Projectile>(() => Instantiate<Projectile>(data.ProjectilePrefab), 
                        OnTakeFromPool, OnReturnedToPool, null, 
                        true, data.InitialPoolCapacity));
            }
        }

        private static void OnTakeFromPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }
        
        private static void OnReturnedToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        public Projectile CreateProjectile(ProjectileType type, Vector3 spawnPosition, Vector3 motionDirection)
        {
            if (_pools.TryGetValue(type, out var pool))
            {
                Projectile projectile = pool.Get();
                projectile.transform.position = spawnPosition;
                projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, motionDirection);
                projectile.Setup(motionDirection);
                projectile.SetPool(pool, 10.0f);
                return projectile;
            }
            else
            {
                return null;
            }
        }
    }
}