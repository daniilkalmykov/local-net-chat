namespace Sources.Scripts.Runtime.Models.Rooms
{
    public interface IRoom
    {
        public string Id { get; }
        public string Name { get; }
        public string Owner { get; }
        public int Participants { get; }

        void Join();
        void Leave();
    }
}