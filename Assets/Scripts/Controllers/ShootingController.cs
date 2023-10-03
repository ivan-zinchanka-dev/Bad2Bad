using UnityEngine;

namespace Controllers
{
    public class ShootingController : MonoBehaviour
    {
        //[SerializeField] private 
        [SerializeField] private float _shootingCooldown = 1.0f;
        
        private float _timeBetweenShots;

        private void Shot()
        {
            
        }

        /*public bool ShootIfReady()
        {
            if (_timeBetweenShots > _shootingCooldown)
            {
                _timeBetweenShots = 0.0f;
            }
        }*/
        
        
    }
}