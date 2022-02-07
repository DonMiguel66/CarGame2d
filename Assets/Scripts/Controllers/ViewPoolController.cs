using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public class ViewPoolController
    {
        private readonly Dictionary<string, ObjectPool> _viewCache = new Dictionary<string, ObjectPool>(15);
        public GameObject Instantiate(GameObject prefab)
        {
            //GameObject gameObject;
            //if (!_viewCache.TryGetValue(prefab.name, out ObjectPool viewPool))
            //{
            //    viewPool = new ObjectPool(prefab);
            //    _viewCache[prefab.name] = viewPool;
            //}

            //gameObject = viewPool.Pop();
            //return gameObject;

            if (_viewCache.TryGetValue(prefab.name, out ObjectPool viewPool))
                return viewPool.Pop();

            _viewCache[prefab.name] = new ObjectPool(prefab);

            return prefab;
        }

        public void Destroy(GameObject value)
        {
            _viewCache[value.name].Push(value);
        }
    }
}
