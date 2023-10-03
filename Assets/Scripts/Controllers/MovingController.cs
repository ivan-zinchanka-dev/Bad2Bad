using UnityEngine;

namespace Controllers
{
    public class MovingController : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 3.0f;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Joystick _motionJoystick;
        
        private Vector2 _motion;
        
        private void MoveByJoystick()
        {
            _motion = _motionJoystick.Direction * _maxSpeed;

            _rigidbody.MovePosition(transform.position + (Vector3)_motion);
            
            if (_motion != Vector2.zero)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, _motionJoystick.Direction);
            }
        }
        
        private void FixedUpdate()
        {
            MoveByJoystick();
        }
    }
}