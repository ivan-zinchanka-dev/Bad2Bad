using System;
using UnityEngine;

namespace Controllers
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;
        //[SerializeField] private Collider2D _selfCollider;

        //public Collider2D Collider => _selfCollider;
        
        public Projectile Setup(Vector2 direction)
        {
            _rigidbody.velocity = direction * _speed;
            return this;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<HealthBody>(out HealthBody healthBody))
            {
                Debug.Log("Yes");
                healthBody.MakeDamage(_damage);
                SelfDestroy();
            }
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}