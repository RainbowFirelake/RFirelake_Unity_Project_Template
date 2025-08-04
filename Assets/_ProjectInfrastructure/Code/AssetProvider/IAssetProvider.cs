using Cysharp.Threading.Tasks;
using RFirelake.Infrastructure.AddressableAddons;
using RFirelake.Infrastructure.Logs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RFirelake.Infrastructure.AssetProvider
{
    public interface IAssetProvider
    {
        public UniTask<TAsset> Load<TAsset>(object key) where TAsset : class;
        public UniTask<TComponent> LoadGameObjectWithComponent<TComponent>(AssetReferenceGameObject<TComponent> assetReference) where TComponent : Component;
        public UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class;
        public UniTask<TAsset[]> LoadAssetsByLabel<TAsset>(string label) where TAsset : class;
        public void ReleaseAll();
        public void Release(AssetReference assetReference) => Release(assetReference.RuntimeKey);
        public void Release(object key);
    }

    public class AddressableAssetProvider : IAssetProvider
    {
        private readonly Dictionary<object, AsyncOperationHandle> assetRequests = new();
        private readonly ILogger<AddressableAssetProvider> _logger;

        public AddressableAssetProvider(ILogger<AddressableAssetProvider> logger)
        {
            _logger = logger;
        }

        public async UniTask<TAsset> Load<TAsset>(object key) where TAsset : class
        {
            if (!assetRequests.TryGetValue(key, out AsyncOperationHandle handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                assetRequests.Add(key, handle);
            }

            await handle.Task;

            return handle.Result as TAsset;
        }

        public async UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class
            => await Load<TAsset>(assetReference.RuntimeKey);

        public async UniTask<TAsset[]> LoadAssetsByLabel<TAsset>(string label) where TAsset : class
        {
            var assetsList = await GetAssetsListByLabel(label);

            var tasks = new List<UniTask<TAsset>>(assetsList.Count);
            foreach (var key in assetsList)
            {
                tasks.Add(Load<TAsset>(key));
            }

            return await UniTask.WhenAll(tasks);
        }

        public async UniTask<TComponent> LoadGameObjectWithComponent<TComponent>(AssetReferenceGameObject<TComponent> assetReference) where TComponent : Component
        {
            var gameObject = await Load<GameObject>(assetReference);
            if (gameObject.TryGetComponent<TComponent>(out var component))
            {
                return component;
            }
            else
            {
                throw new Exception($"Component \"{nameof(TComponent)}\" not found on {gameObject.name}!");
            }
        }

        public void Release(object key)
        {
            throw new System.NotImplementedException();
        }

        private async UniTask<List<string>> GetAssetsListByLabel(string label, Type type = null)
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(label, type);

            var locations = await operationHandle.Task;
            var assetKeys = new List<string>(locations.Count);

            foreach (var location in locations)
                assetKeys.Add(location.PrimaryKey);

            Addressables.Release(locations);
            return assetKeys;
        }

        public void ReleaseAll()
        {
            var releaseCount = 0;

            foreach (var assetRequestKvp in assetRequests)
            {
                if (!assetRequestKvp.Value.IsDone)
                {
                    assetRequestKvp.Value.ReleaseHandleOnCompletion();
                }
                else
                {
                    Addressables.Release(assetRequestKvp.Value);
                }

                releaseCount++;
            }
            _logger.LogDebug($"Released assets count={releaseCount}");
            assetRequests.Clear();
        }
    }
}
