using UnityEngine;

namespace RFirelake.Infrastructure.Extensions
{
    public static class Vector3IntExtensions
    {
        public static Vector3 ConvertToVector3(this Vector3Int vector3Int)
        {
            return new Vector3(vector3Int.x, vector3Int.y, vector3Int.z);
        }
    }
}
