using System;
using System.Diagnostics;


namespace TodoApi.Libs
{
    public class CustomLogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
            Console.WriteLine(message);
        }
    }
}