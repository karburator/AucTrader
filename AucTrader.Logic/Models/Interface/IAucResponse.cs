using System;
using System.Runtime.Serialization;

namespace AucTrader.Logic.Models
{
    /// <summary>Объект ответа по запросу к близовскому api.</summary>
    public interface IAucResponse
    {
        /// <summary>Массив файлов и дат их обновления.</summary>
        AucFile[] Files { get; set; }
    }
}
