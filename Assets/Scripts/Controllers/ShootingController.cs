using System;
using Factories;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField] private float _shootingCooldown = 1.0f;

        [SerializeField] private Transform _shotSourcePoint;
        
        private float _timeBetweenShots;

        private ProjectilesFactory _projectilesFactory;

        [Inject]
        private void InjectDependencies(ProjectilesFactory projectilesFactory)
        {
            _projectilesFactory = projectilesFactory;
        }

        private void Shoot()
        {
            _projectilesFactory.CreateProjectile(_shotSourcePoint.position, transform.up);
        }

        [EasyButtons.Button]
        public void ShootIfReady()
        {
            if (_timeBetweenShots > _shootingCooldown)
            {
                Shoot();
                _timeBetweenShots = 0.0f;
            }
        }

        private void Update()
        {
            _timeBetweenShots += Time.deltaTime;
        }
    }
}