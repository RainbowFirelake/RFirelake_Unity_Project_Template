using UnityEngine;
using VContainer;

namespace RFirelake.Infrastructure.Factories
{
    public class GameObjectFactory
    {
        private readonly IObjectResolver _objectResolver;

        public GameObjectFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public GameObject Create(GameObject prefab)
        {
            return Create(prefab, Vector3.zero, Quaternion.identity, null);
        }

        public GameObject Create(GameObject prefab, Transform parent)
        {
            return Create(prefab, Vector3.zero, Quaternion.identity, parent);
        }

        public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var gameObject = Object.Instantiate(prefab, position, rotation, parent);
            _objectResolver.Inject(gameObject);

            return gameObject;
        }

        public T Create<T>(T prefab) where T : Component
        {
            return Create<T>(prefab, Vector3.zero, Quaternion.identity, null);
        }

        public T Create<T>(T prefab, Transform parent) where T : Component
        {
            return Create<T>(prefab, Vector3.zero, Quaternion.identity, parent);
        }

        public T Create<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
            var gameObject = Object.Instantiate(prefab, position, rotation, parent);
            _objectResolver.Inject(gameObject);

            return gameObject;
        }
    }
}
