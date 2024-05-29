namespace TodoApi.Libs
{
    public interface IMMServer {
        void Release();
    }

    public class MMServer : IMMServer
    {
        public void Release()
        {
            Console.WriteLine("From MMServer");
        }
    }
}