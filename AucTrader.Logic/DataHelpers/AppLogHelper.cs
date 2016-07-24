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
    }
}
