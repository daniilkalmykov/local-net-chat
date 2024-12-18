namespace Sources.Scripts.Runtime.Models.Network.ModelsToSend
{
    public sealed class ModelToSend<T>
    {
        public ModelToSend(Command command, T value)
        {
            Command = (int)command;
            Value = value;
        }

        public int Command { get; private set; }
        public T Value { get; private set; }
    }
}