using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AucTrader.Logic.DataHelpers;

namespace AucTrader.Logic.Models.DataBase
{
    public static class AucTraderDbContextHelper
    {
        /// <summary>Сохраняет лоты пачками, чтобы работало быстрее.</summary>
        public static void SavePositionsAsPackages(List<Position> allPositions)
        {
            AucTraderDbContext context = new AucTraderDbContext();

            // Сохраняем по 300 штук.
            const int MX = 300;
            
            int mx = MX;
            for (int i = 0; i < allPositions.Count; i++)
            {
                Position aucPosition = allPositions[i];
                context.Positions.AddOrUpdate((Position)aucPosition);

                // Пришло ли время сохранять.
                if (i == mx)
                {
                    mx += MX;
                    context.SaveChanges();

                    // Пересоздаем контекст, вроде так еще быстрее работает.
                    context.Dispose();
                    context = new AucTraderDbContext();
                }
            }

            // Сохраняем то, что осталось.
            context.SaveChanges();
            context.Dispose();

            AppLogHelper.TraceInformation(String.Format("Сохранено {0} записей.", allPositions.Count));
        }
    }
}
