using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RFirelake.Infrastructure.AddressableAddons
{
    [System.Serializable]
    public class AssetReferenceGameObject<T> : AssetReferenceGameObject
    {
        public AssetReferenceGameObject(string guid) : base(guid)
        {
        }

        public override bool ValidateAsset(string mainAssetPath)
        {
#if UNITY_EDITOR
            var baseValidation = base.ValidateAsset(mainAssetPath);

            if (!baseValidation)
                return false;

            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(mainAssetPath);
            var isComponentExists = asset.TryGetComponent(out T component);

            return baseValidation && isComponentExists;
#else
            return base.ValidateAsset(mainAssetPath);
#endif
        }
    }
}