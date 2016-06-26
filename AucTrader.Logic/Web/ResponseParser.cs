using System;
using System.Runtime.Serialization.Json;
using System.Text;
using AucTrader.Logic.Models;

namespace AucTrader.Logic.Web
{
    public class ResponseParser : IResponseParser
    {
        public IAucResponse ParseAucResponse(string responseStr)
        {
            IAucResponse response = null;
            
            if (String.IsNullOrEmpty(responseStr))
                return null;

            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(AucResponse));
                response = (AucResponse)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(responseStr)));
            }
            catch (Exception e)
            {
                throw new ApplicationException("Не удалось прочитать ответ с сервера API.", e);
            }
            
            return response;
        }

        public IAucJsonFile ParseAucJsonFile(string responseStr)
        {
            IAucJsonFile response = null;

            if (String.IsNullOrEmpty(responseStr))
                return null;

            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(AucJsonFile));
                response = (AucJsonFile)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(responseStr)));
            }
            catch (Exception e)
            {
                throw new ApplicationException("Не удалось прочитать ответ с сервера API.", e);
            }

            return response;
        }
    }

    public interface IResponseParser
    {
        /// <summary>Парсит JSON ответ сервака на запрос по api данных аука.</summary>
        /// <param name="responseStr">JSON строка.</param>
        /// <returns>Объект модели ответа сервера.</returns>
        IAucResponse ParseAucResponse(string responseStr);

        /// <summary>Парсит JSON файл в объект представляющий модель файла.</summary>
        /// <param name="responseStr">JSON строка.</param>
        /// <returns>Объект модели файла с данными аукциона.</returns>
        IAucJsonFile ParseAucJsonFile(string responseStr);
    }
}