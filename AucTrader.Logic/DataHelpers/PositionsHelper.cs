using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AucTrader.Logic.Models.DataBase;

namespace AucTrader.Logic.DataHelpers
{
    public class PositionsHelper
    {
        public static List<Position> GetLastLoadPositions()
        {
            AucTraderDbContext context = new AucTraderDbContext();

            DateTime mxDate = context.Positions.Max(el => el.LoadDateTime);

            List<Position> lastPositions = context.Positions
                .Where(el => el.LoadDateTime == mxDate)
                .OrderBy(el=>el.Auc)
                .ToList();

            return lastPositions;
        }
    }
}
