namespace Common.Exceptions
{
    public class NotFoundExcpetion : Exception
    {
        public NotFoundExcpetion(string message) : base(message) { }

        public NotFoundExcpetion(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.") { }
    }
}
