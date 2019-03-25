namespace NgNet.Host
{
    public interface IHost
    {
        string Guid { get; }

        Lifetime Lifetime { get; }

        bool IsConsole { get; }

        void Run(); 

        void Stop();
    }
}
