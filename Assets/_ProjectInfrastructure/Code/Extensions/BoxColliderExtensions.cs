using UnityEngine;

namespace RFirelake.Infrastructure.Extensions
{
    public static class BoxColliderExtensions
    {
        public static Vector3 GetRandomPositionInside(this BoxCollider boxCollider)
        {
            var bounds = boxCollider.bounds;
        
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}