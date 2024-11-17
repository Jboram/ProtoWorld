using System;
using UnityEngine;

namespace ProtoWorld
{
    public static class GameUtil
    {
        public static T Bind<T>(Transform parent, string path) where T : Component
        {
            var child = Bind(parent, path);
            if (child.TryGetComponent<T>(out var comp) == false)
            {
                throw new Exception($"Failed to find a component({typeof(T).Name}) from GameObject({path})");
            }

            return comp;
        }
        public static Transform Bind(Transform parent, string path)
        {
            Transform child = parent.Find(path);
            if (child == null)
            {
                throw new Exception($"Failed to find a gameObject in path({path})");
            }
            return child;
        }
    }

}