using System;
using System.Runtime.Serialization;

namespace AucTrader.Logic.Models
{
    [DataContract]
    public class AucRealm
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "slug")]
        public string Slug { get; set; }
    }
}
