using System;

namespace AucTrader.Logic.Models
{
    /// <summary>Объект файла, который возвращает близовское api.</summary>
    public interface IAucFile
    {
        /// <summary>Url, по которому можно качать JSON с данными аукциона.</summary>
        string Url { get; set; }
        /// <summary>Дата и времы последнего обновления файла из Url.</summary>
        Int64 LastModified { get; set; }
    }
}
