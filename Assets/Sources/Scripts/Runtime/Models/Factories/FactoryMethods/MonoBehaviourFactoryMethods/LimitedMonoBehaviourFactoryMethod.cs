using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MonoBehaviourFactoryMethods
{
    internal sealed class LimitedMonoBehaviourFactoryMethod : IMonoBehaviourFactoryMethod
    {
        private readonly GameObject _prefab;
        private readonly Vector3 _position;
        private readonly Transform _parent;
        private readonly int _limit;
        private readonly List<GameObject> _container = new();

        public LimitedMonoBehaviourFactoryMethod(GameObject prefab, Vector3 position, Transform parent, int limit)
        {
            _prefab = prefab;
            _position = position;
            _parent = parent;
            _limit = limit;
        }

        public async UniTask<T> CreateInMainThread<T>()
        {
            await UniTask.SwitchToMainThread();

            if (_limit == _container.Count)
            {
                Object.Destroy(_container[0]);
                _container.RemoveAt(0);
            }

            if (_prefab.GetComponent<T>() == null)
                return default;

            var gameObject = Object.Instantiate(_prefab, _position, Quaternion.identity, _parent);

            _container.Add(gameObject);
            
            return gameObject.GetComponent<T>();
        }
    }
}