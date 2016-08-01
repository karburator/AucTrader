using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AucTrader.Logic.Models.DataBase;

namespace AucTrader.Logic.DataHelpers
{
    public static class AppLogHelper
    {
        private static AucTraderDbContext context = new AucTraderDbContext();

        public static void TraceInformation(string message)
        {
            AppLog log = new AppLog();

            log.LogType = "Информация";
            log.Message = message;

            context.AppLogs.Add(log);
            context.SaveChanges();
        }

        public static void TraceError(string message, Exception e)
        {
            AppLog log = new AppLog();

            log.LogType = "Ошибка";
            log.Message = message+"\n"+ e.StackTrace;

            context.AppLogs.Add(log);
            context.SaveChanges();
        }
    }
}
