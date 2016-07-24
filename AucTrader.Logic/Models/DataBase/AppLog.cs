using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AucTrader.Logic.Models.DataBase
{
    public class AppLog
    {
        public int Id { get; set; }
        public string LogType { get; set; }
        public string Module { get; set; }
        public string Message { get; set; }
        public string Note { get; set; }
    }
}
