using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AucTrader.Logic.DataHelpers;
using AucTrader.Logic.Models;
using AucTrader.Logic.Models.DataBase;
using AucTrader.Logic.Web;

namespace AucApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file = GetAucJsonFile();

            AucTraderDbContextHelper.SavePositionsAsPackages(file.AucPositions.ToList());
        }

        private IAucJsonFile GetAucJsonFile()
        {
            AucDataLoader loader = new AucDataLoader();

            DateTime lastModifyDate;
            IAucJsonFile file = loader.GetAucJsonFile(out lastModifyDate);

            foreach (var position in file.AucPositions)
            {
                position.LoadDateTime = lastModifyDate;
            }
            return file;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateData2();
        }

        private void UpdateData()
        {
            try
            {
                Dictionary<long, Position> oldPositions = PositionsHelper.GetLastLoadPositions()
                    .ToDictionary(el => el.Auc);

                IAucJsonFile file = GetAucJsonFile();
                List<Position> newPositions = file.AucPositions.ToList();

                List<Position> oldHardPositions = new List<Position>();
                List<Position> newHardPositions = new List<Position>();
                List<Position> toUpdatePositions = new List<Position>();

                foreach (Position newPos in newPositions)
                {
                    Position oldPos = null;
                    // Если не получилось найти, значит лот новый (раньше такого лота не было, позможно перевыставили).
                    if (!oldPositions.TryGetValue(newPos.Auc, out oldPos))
                    {
                        newHardPositions.Add(newPos);
                    }
                }

                // Обновляем старые лоты.
                AucTraderDbContextHelper.SavePositionsAsPackages(newHardPositions);
            }
            catch (Exception)
            {
            }
        }

        private void UpdateData2()
        {
            try
            {
                Dictionary<long, Position> oldPositions = PositionsHelper.GetLastLoadPositions2()
                    .ToDictionary(el => el.Auc);

                IAucJsonFile file = GetAucJsonFile();
                List<Position> newPositions = file.AucPositions.ToList();

                Dictionary<long, Position> toUpdatePositions = new Dictionary<long, Position>();
                List<Position> newHardPositions = new List<Position>();
                List<Position> oldHardPositions = new List<Position>();

                DateTime withdrwanDateTime = newPositions.First().LoadDateTime;
                foreach (Position newPos in newPositions)
                {
                    Position oldPos = null;
                    // Если не получилось найти, значит лот новый (раньше такого лота не было, позможно перевыставили).
                    if (!oldPositions.TryGetValue(newPos.Auc, out oldPos))
                    {
                        // Не нашли старой позиции, значит новая, может быть перевыставили и пох, пока что.
                        newHardPositions.Add(newPos);
                        continue;
                    }

                    // Нашли старую идентичную позицию, обновим ее новыми даннымы.
                    if (oldPos != null)
                    {
                        newPos.Id = oldPos.Id;
                        toUpdatePositions.Add(newPos.Auc, newPos);
                    }
                }

                foreach (var oldPos in oldPositions.Values)
                {
                    Position pos;
                    if (!toUpdatePositions.TryGetValue(oldPos.Auc, out pos))
                    {
                        // Не смогли найти, значит лот выбыл.
                         oldPos.WithdrawnDateTime = withdrwanDateTime;
                        oldHardPositions.Add(oldPos);
                    }
                }

                // Закрываем старые лоты.
                AucTraderDbContextHelper.SavePositionsAsPackages(oldHardPositions);

                // Обновляем старые данные.
                AucTraderDbContextHelper.SavePositionsAsPackages(toUpdatePositions.Values.ToList());

                // Обновляем старые лоты.
                AucTraderDbContextHelper.SavePositionsAsPackages(newHardPositions);
            }
            catch (Exception e)
            {
                AppLogHelper.TraceError("", e);
            }
        }
    }
}
