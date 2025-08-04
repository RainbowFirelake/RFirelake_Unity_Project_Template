using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RFirelake.Infrastructure.AddressableAddons
{
    [System.Serializable]
    public class AssetReferenceMaterial : AssetReferenceT<Material>
    {
        public AssetReferenceMaterial(string guid) : base(guid)
        {
        }

        public override bool ValidateAsset(string path)
        {
#if UNITY_EDITOR
            if (AssetDatabase.GetMainAssetTypeAtPath(path) == typeof(Material))
                return true;
#endif
            return false;
        }
    }
}