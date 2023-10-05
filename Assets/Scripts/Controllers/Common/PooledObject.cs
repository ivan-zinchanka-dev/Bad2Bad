using UnityEngine;
using UnityEngine.Pool;

namespace Controllers.Common
{
    public class PooledObject<T> : MonoBehaviour where T : class
    {
        private IObjectPool<T> _objectPool;
        private float _lifetime;
        private bool _isReleased;
        
        public void Refresh(IObjectPool<T> objectPool, float maxLifetime = float.MaxValue)
        {
            _objectPool = objectPool;
            _lifetime = maxLifetime;
            _isReleased = false;
            Invoke(nameof(Release), _lifetime);
        }

        protected void Release()
        {
            if (!_isReleased && this is T obj)
            {
                CancelInvoke(nameof(Release));
                _objectPool.Release(obj);
                _isReleased = true;
            }
        }
    }
}