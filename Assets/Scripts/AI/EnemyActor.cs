﻿using System;
using Controllers;
using Factories;
using Management;
using UnityEngine;
using NPBehave;
using UnityEngine.Serialization;
using Zenject;
using Action = NPBehave.Action;

namespace AI
{
    public class EnemyActor : MonoBehaviour
    {
        [SerializeField] private float _viewingRange;
        [SerializeField] private float _shootingRange;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _minDistance;
        
        [SerializeField] private HealthBody _healthBody;
        [SerializeField] private ShootingController _shootingController;
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private GameLevelManager _gameLevelManager;
        private ProjectilesFactory _projectilesFactory;
        
        private Root _behaviorTree;

        
        
        [Inject]
        private void InjectDependencies(GameLevelManager gameLevelManager, ProjectilesFactory projectilesFactory)
        {
            _gameLevelManager = gameLevelManager;
            _projectilesFactory = projectilesFactory;
        }
        
        private bool IsAlive()
        {
            Debug.Log("IsAlive: " + (_healthBody.Health > 0));
            
            return _healthBody.Health > 0;
        }
        
        private bool SeesThePlayer()
        {
            Transform playerTransform = _gameLevelManager.Player.transform;
            return Vector2.Distance(playerTransform.position, transform.position) < _viewingRange;
        }

        private bool CanShootThePlayer()
        {
            Transform playerTransform = _gameLevelManager.Player.transform;
            return Vector2.Distance(playerTransform.position, transform.position) < _shootingRange;
        }

        private void MoveToPlayer()
        {
            Transform playerTransform = _gameLevelManager.Player.transform;
            
            Vector3 movingDirection = (playerTransform.position - transform.position).normalized;
            
            transform.rotation = Quaternion.LookRotation(Vector3.forward, movingDirection);

            if (Vector2.Distance(playerTransform.position, transform.position) > _minDistance)
            {
                _rigidbody.velocity = movingDirection * _movingSpeed;
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }

        private void StartShootingThePlayer()
        {
            Debug.Log("Start shooting");
            
            Transform playerTransform = _gameLevelManager.Player.transform;
            _shootingController.StartShooting(playerTransform);
        }
        
        private void StopShootingThePlayer()
        {
            Debug.Log("Stop shooting");
            
            _shootingController.StopShooting();
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public void TargetNode()
        {
            Debug.Log("TargetNode");
        }
        
        void Start()
        {

            _behaviorTree = new Root(

                new Selector(
                    new Condition(IsAlive, new Selector(
                        new Condition(SeesThePlayer, new Sequence(
                            
                            //new Action(()=>Debug.Log("Traget"))
                            
                            new Action(MoveToPlayer),
                            new Selector(
                                new Condition(CanShootThePlayer, new Action(StartShootingThePlayer)),
                                new Action(StopShootingThePlayer)
                            )
                        
                        )),
                        
                        new Action(()=>Debug.Log("Idle"))
                    )),

                    new Action(SelfDestroy)
                )
            );
            
            
            _behaviorTree.Start();
        }

        private void OnDestroy()
        {
            if (_behaviorTree != null && _behaviorTree.CurrentState == Node.State.ACTIVE)
            {
                _behaviorTree.Stop();
            }
        }
    }
    
    
    
}