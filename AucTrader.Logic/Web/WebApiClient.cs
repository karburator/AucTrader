using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AucTrader.Logic.Web
{
    public class WebApiClient : IWebApiClient
    {
        public WebApiClient(string locale, string apiKey)
        {
            Locale = locale;
            ApiKey = apiKey;
        }

        private const string ApiUrl = "https://eu.api.battle.net/wow/auction/data/deathguard";
        private const string LocaleParamName = "locale";
        private const string ApiKeyParamName = "apikey";

        public string Locale { get; private set; }
        public string ApiKey { get; private set; }

        public string GetAucResponseStr()
        {
            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                client.QueryString.Add(LocaleParamName, Locale);
                client.QueryString.Add(ApiKeyParamName, ApiKey);

                string responseStr = client.DownloadString(ApiUrl);
                return responseStr;
            }
            catch (WebException e)
            {
                throw new ApplicationException(e.Message, e);
            }
        }

        public string GetAucFile(string url)
        {
            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string responseStr = client.DownloadString(url);
                return responseStr;
            }
            catch (WebException e)
            {
                throw new ApplicationException(e.Message, e);
            }
        }
    }

    /// <summary>������, ����������� ������� � api �������.</summary>
    public interface IWebApiClient
    {
        /// <summary>��������� ������ ������ � webapi.</summary>
        /// <returns>JSON ����� �������, ���������� ����� ����� � �������.</returns>
        string GetAucResponseStr();
        /// <summary>��������� � ������ ���� ������ �������� � ���� ������.</summary>
        /// <param name="url">�����, ������� ������ ������ � api.</param>
        /// <returns>���� ������ �������� � ���� ������.</returns>
        string GetAucFile(string url);
    }
}