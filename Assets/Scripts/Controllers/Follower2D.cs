using UnityEngine;

namespace Controllers
{
    public class Follower2D : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] [Range(0.0f, 100.0f)] private float _interpolationSpeed = 100.0f;

        private Vector2 _cachedPosition;
        private Vector2 _delta;

        public void SetTarget(Transform target)
        {
            _target = target;
            _delta = _target.position - transform.position;
        }

        private void Start()
        {
            _delta = _target.position - transform.position;
        }

        private void Update()
        {
            float interpolation = _interpolationSpeed * Time.deltaTime;
            
            Vector2 targetPosition = _target.position;
            
            _cachedPosition = transform.position;
            _cachedPosition = Vector2.Lerp(_cachedPosition, targetPosition - _delta, interpolation);
            transform.position = _cachedPosition;
        }
    }
}