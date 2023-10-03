using Controllers;
using UnityEngine;

namespace Factories
{
    public class ProjectilesFactory : MonoBehaviour
    {
        [SerializeField] private Projectile _defaultProjectilePrefab;

        public Projectile CreateProjectile(Vector3 spawnPosition, Vector3 motionDirection)
        {
            return Instantiate(_defaultProjectilePrefab, spawnPosition, 
                    Quaternion.LookRotation(Vector3.forward, motionDirection)).Setup(motionDirection);
        }
    }
}