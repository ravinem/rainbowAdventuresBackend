using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Threading;


namespace logging_study
{
    public class Program
    {
        //Declare an instance for log4net
        private static readonly ILog Log =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static void logdebug(string message)
        {
            Log.Debug(message);
        }

        public static void loginfo(string info)
        {
            Log.Info(info);
        }

        public static void logerror(string error)
        {
            Log.Error(error);
        }
    }
}
    

