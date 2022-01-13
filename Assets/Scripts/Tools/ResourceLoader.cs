using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame2D
{
    public static  class ResourceLoader
    {
        public static GameObject LoadPrefab(ResourcePath path)
        {
            return Resources.Load<GameObject>(path.PathResources);
        }

        internal static T LoadObject<T>(ResourcePath path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path.PathResources);
        }
    }
}
