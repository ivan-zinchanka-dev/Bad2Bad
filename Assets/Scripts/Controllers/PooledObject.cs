using UnityEngine;
using UnityEngine.Pool;

namespace Controllers
{
    public class PooledObject<T> : MonoBehaviour where T : class
    {
        private IObjectPool<T> _objectPool;
        
        public void SetPool(IObjectPool<T> objectPool)
        {
            _objectPool = objectPool;
        }

        public void Release()
        {
            if (this is T obj)
            {
                _objectPool.Release(obj);
            }
        }
    }
}