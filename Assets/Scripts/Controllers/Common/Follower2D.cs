using UnityEngine;

namespace Controllers.Common
{
    public class Follower2D : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] [Range(0.0f, 100.0f)] private float _interpolationSpeed = 100.0f;
        [SerializeField] private bool _destroyWithTarget;
        
        private Vector2 _cachedPosition;
        private Vector2 _delta;
        private Vector3 _cached3DPosition;
        
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
            if (_target == null)
            {
                if (_destroyWithTarget)
                {
                    SelfDestroy();
                }
                else 
                    return;
            }
            else
            {
                float interpolation = _interpolationSpeed * Time.deltaTime;
            
                Vector2 targetPosition = _target.position;
            
                _cachedPosition = transform.position;
                _cachedPosition = Vector2.Lerp(_cachedPosition, targetPosition - _delta, interpolation);

                _cached3DPosition.x = _cachedPosition.x;
                _cached3DPosition.y = _cachedPosition.y;
                _cached3DPosition.z = transform.position.z;
                
                transform.position = _cached3DPosition;
            }
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}