using System;
using System.Runtime.Serialization;
using AucTrader.Logic.Models.DataBase;

namespace AucTrader.Logic.Models
{
    [DataContract]
    public class AucJsonFile : IAucJsonFile
    {
        [DataMember(Name = "realms")]
        public AucRealm[] AucRealms { get; set; }

        [DataMember(Name = "auctions")]
        public Position[] AucPositions { get; set; }
    }

    public interface IAucJsonFile
    {
        AucRealm[] AucRealms { get; set; }
        Position[] AucPositions { get; set; }
    }
}
