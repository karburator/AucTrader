using System;
using System.Reflection;
using System.Resources;
using AucTrader.Logic.Models;
using AucTrader.Logic.Properties;

namespace AucTrader.Logic.Web
{
    public class AucDataLoader : IAucDataLoader
    {
        public IAucResponse GetAucResponse()
        {
            IAucResponse response = null;
            IWebApiClient webApiClient = new WebApiClient(Resources.Locale, Resources.ApiKey);
            string responseStr = webApiClient.GetAucResponseStr();

            IResponseParser parser = new ResponseParser();
            response = parser.ParseAucResponse(responseStr);

            return response;
        }

        public IAucJsonFile GetAucJsonFile()
        {
            IAucJsonFile response = null;
            IAucResponse aucResponse = GetAucResponse();

            IWebApiClient webApiClient = new WebApiClient(Resources.Locale, Resources.ApiKey);
            string file = webApiClient.GetAucFile(aucResponse.Files[0].Url);

            IResponseParser parser = new ResponseParser();
            response = parser.ParseAucJsonFile(file);

            return response;
        }
    }

    public interface IAucDataLoader
    {
        IAucResponse GetAucResponse();
        IAucJsonFile GetAucJsonFile();
    }
}
