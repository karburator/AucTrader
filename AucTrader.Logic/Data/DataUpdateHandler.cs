using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AucTrader.Logic.Models.DataBase;
using AucTrader.Logic.Web;

namespace AucTrader.Logic.Data
{
    public class DataUpdateHandler
    {
        public void UpdateData(Position[] positions, Int64 lastModifiedDate, AucTraderDbContext db)
        {
            const int MX = 300;
            int mx = MX;
            DateTime baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime datetime = baseDateTime.AddMilliseconds(lastModifiedDate).ToLocalTime();

            for (int i = 0; i < positions.Length; i++)
            {
                Position aucPosition = positions[i];
                aucPosition.LoadDateTime = datetime;

                db.Positions.Add(aucPosition);

                if (i == mx)
                {
                    mx += MX;
                    db.SaveChanges();
                    db.Dispose();
                    db = new AucTraderDbContext();
                }
            }

            db.SaveChanges();
        }


    }
}
