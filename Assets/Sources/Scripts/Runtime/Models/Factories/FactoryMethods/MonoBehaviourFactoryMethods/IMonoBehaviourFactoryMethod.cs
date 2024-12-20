#nullable enable
using Cysharp.Threading.Tasks;

namespace Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MonoBehaviourFactoryMethods
{
    public interface IMonoBehaviourFactoryMethod
    {
        UniTask<T?> CreateInMainThread<T>();
    }
}