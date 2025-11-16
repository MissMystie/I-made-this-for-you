using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystie.Utils
{
    internal static class GameObjectExtension
    {
        public static bool IsInLayerMask(this GameObject gameObject, LayerMask mask)
        {
            return (mask == (mask | (1 << gameObject.layer)));
        }

        public static void ShuffleChildren(this Transform t)
        {
            List<Transform> children = new List<Transform>();

            // Get all children of the parent
            foreach (Transform child in t)
            {
                children.Add(child);
            }

            children.Shuffle();

            // Reorder the children based on the randomized order
            foreach (Transform child in children)
            {
                child.SetAsLastSibling();
            }
        }
    }
}
