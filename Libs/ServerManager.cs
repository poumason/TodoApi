using NLog.Extensions.Logging;

namespace TodoApi.Libs
{
    public class ServerManager
    {

        public void Do(bool status)
        {
            dynamic mgr = 1;
            try
            {
                if (status)
                {
                    mgr = getMMInstance();
                }
                else
                {
                    mgr = getT01Instance();
                }
            }
            finally
            {
                if (mgr.getTupe() != typeof(int)) {
                    mgr.Release();
                }
            }
        }

        public T ChangeServer<T>()
        {
            var mgr = default(T);
            if (typeof(T) is IMMServer)
            {
                mgr = (T?)getMMInstance();
            }
            else if (typeof(T) is IT01Server)
            {
                mgr = (T?)getT01Instance();
            }
            return mgr;
        }

        private IMMServer getMMInstance()
        {
            return new MMServer();
        }

        private IT01Server getT01Instance()
        {
            return new T01Server();
        }
    }
}