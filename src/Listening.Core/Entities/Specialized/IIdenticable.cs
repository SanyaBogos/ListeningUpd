namespace Listening.Server.Entities.Specialized
{
    public interface IIdenticable<T>
    {
        T Id { get; }
    }
}
