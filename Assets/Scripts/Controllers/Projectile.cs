using System;
using UnityEngine;

namespace Controllers
{
    public class Projectile : PooledObject<Projectile>
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;
        
        public Projectile Setup(Vector2 direction)
        {
            _rigidbody.velocity = direction * _speed;
            return this;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<HealthBody>(out HealthBody healthBody))
            {
                healthBody.MakeDamage(_damage);
                Release();
            }
        }

        /*private void Release()
        {
            Destroy(gameObject);
        }*/
    }
}