using UnityEngine;

    public static class TransformExtensions
    {
        public static float UniformedLossyScale(this Transform transform)
        {
            return Mathf.Max(
                Mathf.Max(transform.lossyScale.x,
                transform.lossyScale.y),
                transform.lossyScale.z
            );
        }
    }