using Microsoft.Extensions.Logging;

namespace TodoApi.Libs
{
    public class ServerManager
    {
        private readonly ILogger _logger;

        public ServerManager(ILogger logger)
        {
            _logger = logger;
            
            var x = NLog.LogManager.GetCurrentClassLogger();
        }

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
                if (mgr.GetType() != typeof(int))
                {
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
            _logger.Log(LogLevel.Information, "getMMInstance");
            return new MMServer();
        }

        private IT01Server getT01Instance()
        {
            _logger.Log(LogLevel.Information, "getT01Instance");
            return new T01Server();
        }
    }
}