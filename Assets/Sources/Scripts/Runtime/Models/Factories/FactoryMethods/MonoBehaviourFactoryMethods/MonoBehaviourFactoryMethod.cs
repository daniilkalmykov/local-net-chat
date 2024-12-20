#nullable enable
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MonoBehaviourFactoryMethods
{
    internal sealed class MonoBehaviourFactoryMethod : IMonoBehaviourFactoryMethod
    {
        private readonly GameObject _prefab;
        private readonly Vector3 _position;
        private readonly Transform _parent;

        public MonoBehaviourFactoryMethod(GameObject prefab, Vector3 position, Transform parent)
        {
            _prefab = prefab;
            _position = position;
            _parent = parent;
        }

        public async UniTask<T?> CreateInMainThread<T>()
        {
            await UniTask.SwitchToMainThread();

            if (_prefab.GetComponent<T>() == null)
                return default;

            var gameObject = Object.Instantiate(_prefab, _position, Quaternion.identity, _parent);

            return gameObject.GetComponent<T>();
        }
    }
}