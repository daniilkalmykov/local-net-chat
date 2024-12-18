using System;

namespace Sources.Scripts.Runtime.Models.Network
{
    public interface ICommandsReceiver
    {
        event Action<byte[]> Received;
        
        void Start();
        void Stop();
    }
}