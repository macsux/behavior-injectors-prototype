using System;

namespace InjectionModule
{
    public class ServerInterface : MarshalByRefObject
    {
        /// <summary>
        /// Called to confirm that the IPC channel is still open / host application has not closed
        /// </summary>
        public void Message(string msg)
        {
            Console.WriteLine(msg);
        }
    }
    
    
}