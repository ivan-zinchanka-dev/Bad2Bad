using UnityEngine;
using UnityEngine.Pool;

namespace Controllers
{
    public class PooledObject<T> : MonoBehaviour where T : class
    {
        private IObjectPool<T> _objectPool;
        private float _lifetime;
        
        public void SetPool(IObjectPool<T> objectPool, float maxLifetime = float.MaxValue)
        {
            _objectPool = objectPool;
            _lifetime = maxLifetime;
            Invoke(nameof(Release), _lifetime);
        }

        protected void Release()
        {
            if (this is T obj)
            {
                CancelInvoke(nameof(Release));
                _objectPool.Release(obj);
            }
        }
    }
}