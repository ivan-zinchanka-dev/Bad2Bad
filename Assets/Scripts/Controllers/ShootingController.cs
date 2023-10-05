using System.Collections;
using Factories;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField] private ProjectileType _projectileType;
        [SerializeField] private float _shootingCooldown = 1.0f;
        
        [SerializeField] private Transform _shotSourcePoint;
        
        private float _timeBetweenShots;
        private ProjectilesFactory _projectilesFactory;
        private Coroutine _shootingRoutine;
        
        [Inject]
        private void InjectDependencies(ProjectilesFactory projectilesFactory)
        {
            _projectilesFactory = projectilesFactory;
        }
        
        [EasyButtons.Button]
        public void ShootIfReady()
        {
            ShootIfReady(null);
        }
        
        public void ShootIfReady(Transform target)
        {
            if (_timeBetweenShots > _shootingCooldown)
            {
                Shoot(target);
                _timeBetweenShots = 0.0f;
            }
        }

        private void Shoot(Transform target)
        {
            if (target == null)
            {
                _projectilesFactory.CreateProjectile(_projectileType, _shotSourcePoint.position, transform.up);
            }
            else
            {
                Vector2 projectileMotionDirection = (target.position - transform.position).normalized;
                _projectilesFactory.CreateProjectile(_projectileType, _shotSourcePoint.position, projectileMotionDirection);
            }
        }
        
        public void StartShooting(Transform target)
        {
            _shootingRoutine ??= StartCoroutine(Shooting(target));
        }

        private IEnumerator Shooting(Transform target)
        {
            WaitForSeconds waitForShootingCooldown = new WaitForSeconds(_shootingCooldown);

            while (true)
            {
                yield return waitForShootingCooldown;
                Shoot(target);
            }
        }

        public void StopShooting()
        {
            if (_shootingRoutine != null)
            {
                StopCoroutine(_shootingRoutine);
                _shootingRoutine = null;
            }
        }

        private void Update()
        {
            _timeBetweenShots += Time.deltaTime;
        }
    }
}