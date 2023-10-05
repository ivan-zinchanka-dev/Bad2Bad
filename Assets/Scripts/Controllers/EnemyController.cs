using UnityEngine;
using NPBehave;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private HealthBody _healthBody;
        
        private Root _behaviorTree;


        public bool IsAlive()
        {
            Debug.Log("IsAlive: " + (_healthBody.Health > 0));
            
            return _healthBody.Health > 0;
        }

        public void SelfDestroy()
        {
            Debug.Log("SelfDestroy");
        }

        void Start()
        {

            _behaviorTree = new Root(

                new Selector(
                    new Action(IsAlive),
                    new Action(SelfDestroy)
                    )
                
                
                
                
                
                );
            
            
            _behaviorTree.Start();
        }
    }
    
    
    
}