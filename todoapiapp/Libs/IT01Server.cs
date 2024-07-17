namespace TodoApi.Libs
{
    public interface IT01Server
    {
        void Release();
    }

    public class T01Server : IT01Server
    {
        public void Release()
        {
            Console.WriteLine("From T01Server");
        }
    }
}