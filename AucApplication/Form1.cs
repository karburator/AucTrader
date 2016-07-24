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
            try
            {
                List<Position> oldPositions = PositionsHelper.GetLastLoadPositions();

                IAucJsonFile file = GetAucJsonFile();
                List<Position> newPositions = file.AucPositions
                    .OrderBy(el => el.Auc)
                    .ToList();

                List<Position> oldHardPositions = new List<Position>();
                List<Position> newHardPositions = new List<Position>();
                List<Position> toUpdatePositions = new List<Position>();

                // Так как новый список отличается от старого новыми лотами или перевыставленными (их номера явно больше последнего номера старого лота)
                // и теми лотами, что были проданы (их номера в списке новых лотов будут тоже отсутствовать),
                // то отсортируем новые и старые лоты по номерам.
                // Далее будем идти по списку старых лотов, 
                // - если старый лот совпадает с очередным новым лотом, то возможно старый лот надо обновить, положим новый лот в toUpdatePositions
                // - если номер очереного старого лота не равен номеру нового, значит лот выбыл (продан или перевыставлен), добавим такой старый лот в oldHardPositions.
                // После прохода по старым лота, останется список новых лотов, с новыми номерами, его ложим в newHardPositions, будем потом искать перевыставленные лоты.
                int j = 0;
                for (int i = 0; i < oldPositions.Count; i++, j++)
                {
                    Position oldPos = oldPositions[i];

                    if (j > oldPositions.Count)
                        break;
                    Position newPos = newPositions[j];

                    while (oldPos.Auc != newPos.Auc)
                    {
                        oldHardPositions.Add(oldPos);
                        oldPos = oldPositions[++j];
                    }

                    toUpdatePositions.Add(newPos);
                }

                // Оставшиеся новые лоты ложим в другую кучу, чтобы потом мерджить.
                for (int i = j; i < newPositions.Count; i++)
                {
                    newHardPositions.Add(newPositions[i]);
                }




                // Обновляем старые лоты.
                AucTraderDbContextHelper.SavePositionsAsPackages(newHardPositions);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
