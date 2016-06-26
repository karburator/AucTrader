using System;
using System.Runtime.Serialization;

namespace AucTrader.Logic.Models
{
    [DataContract]
    public class AucResponse : IAucResponse
    {
        [DataMember(Name = "files")]
        public AucFile[] Files { get; set; }
    }
}
