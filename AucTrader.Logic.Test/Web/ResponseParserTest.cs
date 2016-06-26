using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AucTrader.Logic.Models;
using AucTrader.Logic.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AucTrader.Logic.Test.Web
{
    /// <summary>
    /// Summary description for ResponseParserTest
    /// </summary>
    [TestClass]
    public class ResponseParserTest
    {
        public ResponseParserTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        private const string AucApiFileName = "AucApiResponce.json";
        private const string AucJsonFileName = "AucJsonFile.json";
        
        private static string aucApiResponse;
        private static string aucJsonFile;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dataPath = Path.Combine(baseDirectory, @"../../Web/TestData/");

            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine(dataPath, AucApiFileName)))
                    aucApiResponse = reader.ReadToEnd();

                using (StreamReader reader = new StreamReader(Path.Combine(dataPath, AucJsonFileName)))
                    aucJsonFile = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Не удалось подготовить данные для ResponseParserTest", e);
            }
        }

        [TestMethod]
        public void ParseAucResponseTest()
        {
            // Проверяет парсится ли отсет от сервака на запрос данных аука.

            // Arange
            IAucResponse aucResponse = null;
            ResponseParser parser = new ResponseParser();

            // Act
            aucResponse = parser.ParseAucResponse(aucApiResponse);

            // Assert
            Assert.IsNotNull(aucResponse, "ParseAucResponseTest. Получен пустой ответ.");
            Assert.IsNotNull(aucResponse.Files, "ParseAucResponseTest. Получен пустой ответ для Files.");
            Assert.AreNotEqual(aucResponse.Files.Count(), String.Format("ParseAucResponseTest. Прочитали Files {0}, а должно быть {1}", aucResponse.Files.Count(), 1));

            string url = @"http://auction-api-eu.worldofwarcraft.com/auction-data/4dd7055b110e4cfb3db4c4cc3b3341f3/auctions.json";
            Assert.AreNotSame(url, aucResponse.Files.First().Url);
            Assert.AreNotSame("1466319264000", aucResponse.Files[0].LastModified);
        }

        [TestMethod]
        public void ParseAucJsonFileTest()
        {
            // Arange
            IAucJsonFile jsonFile = null;
            ResponseParser parser = new ResponseParser();

            // Act
            jsonFile = parser.ParseAucJsonFile(aucJsonFile);

            // Assert
            Assert.IsNotNull(jsonFile);
            Assert.AreEqual("Deathguard", jsonFile.AucRealms[0].Name, "Неверное название реалма");
            Assert.AreEqual("deathguard", jsonFile.AucRealms[0].Slug, "Неверный slug");
            Assert.AreNotEqual(jsonFile.AucPositions.Count(), String.Format("ParseAucResponseTest. Прочитали Files {0}, а должно быть {1}", jsonFile.AucPositions.Count(), 1));

            Assert.AreEqual(1525884116, jsonFile.AucPositions[0].Auc, "Неверный Auc");
            Assert.AreEqual(110640, jsonFile.AucPositions[0].Item);
            Assert.AreEqual("Тест", jsonFile.AucPositions[0].Owner);
            Assert.AreEqual("Тест", jsonFile.AucPositions[0].OwnerRealm);
            Assert.AreEqual(1000000, jsonFile.AucPositions[0].Bid);
            Assert.AreEqual(1000000, jsonFile.AucPositions[0].BuyOut);
            Assert.AreEqual(1, jsonFile.AucPositions[0].Quantity);
            Assert.AreEqual("LONG", jsonFile.AucPositions[0].TimeLeft);
            Assert.AreEqual(0, jsonFile.AucPositions[0].Rand);
            Assert.AreEqual(0, jsonFile.AucPositions[0].Seed);
            Assert.AreEqual(0, jsonFile.AucPositions[0].Context);
        }
    }
}
