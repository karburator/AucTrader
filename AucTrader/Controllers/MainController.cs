using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AucTrader.Logic.Models;
using AucTrader.Logic.Web;
using AucTrader.Logic.Models.DataBase;
using AucTrader.Models;
using WebGrease.Css.Extensions;

namespace AucTrader.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update()
        {
            UpdateModel model = new UpdateModel()
            {
                Status = "Fail"
            };

            IAucDataLoader aucDataLoader = new AucDataLoader();
            model.Data = aucDataLoader.GetAucResponse();

            var file = aucDataLoader.GetAucJsonFile();

            Task task = new Task(() =>
            {
                AucTraderDbContext db = new AucTraderDbContext();
                const int MX = 300;
                int mx = MX;
                DateTime baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime datetime = baseDateTime.AddMilliseconds(model.Data.Files[0].LastModified).ToLocalTime();

                StringBuilder builder = new StringBuilder();
                builder.Append("select * from Position where Auc in (");
                Int64 last = file.AucPositions.Last().Auc;
                file.AucPositions
                    .ForEach(el =>
                    {
                        builder.Append(el.Auc);
                        if (el.Auc != last)
                            builder.Append(", ");
                    });
                builder.Append(")");

                //Dictionary<Int64, Position> oldPositions = db.Positions
                //    .SqlQuery(builder.ToString())
                //    .ToDictionary(el => el.Auc);

                for (int i = 0; i < file.AucPositions.Length; i++)
                {
                    Position aucPosition = file.AucPositions[i];
                    aucPosition.LoadDateTime = datetime;

                    //if (oldPositions.ContainsKey(aucPosition.Auc))
                    //    continue;

                    //Position position = aucPosition;
                    //position.LoadDateTime = datetime;

                    //Position position = new Position()
                    //{
                    //    Auc = aucPosition.Auc,
                    //    Bid = aucPosition.Bid,
                    //    BuyOut = aucPosition.BuyOut,
                    //    Context = aucPosition.Context,
                    //    Item = aucPosition.Item,
                    //    Owner = aucPosition.Owner,
                    //    OwnerRealm = aucPosition.OwnerRealm,
                    //    Quantity = aucPosition.Quantity,
                    //    Rand = aucPosition.Rand,
                    //    Seed = aucPosition.Seed,
                    //    TimeLeft = aucPosition.TimeLeft,
                    //    LoadDateTime = datetime
                    //};

                    db.Positions.Add((Position) aucPosition);

                    if (i == mx)
                    {
                        mx += MX;
                        db.SaveChanges();
                        db.Dispose();
                        db = new AucTraderDbContext();
                    }
                }

                db.SaveChanges();
            });

            task.Start();

            return View("Index", model);
        }
    }
}
