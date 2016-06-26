using System;
using System.Runtime.Serialization;

namespace AucTrader.Logic.Models
{
    [DataContract]
    public class AucFile : IAucFile
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "lastModified")]
        public Int64 LastModified { get; set; }
    }
}
