using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AucTrader.Logic.Models.DataBase
{
    [DataContract]
    public class Position
    {
        [IgnoreDataMember]
        public int Id { get; set; }

        [IgnoreDataMember]
        public DateTime LoadDateTime { get; set; }

        [IgnoreDataMember]
        public DateTime? WithdrawnDateTime { get; set; }

        [DataMember(Name = "auc")]
        public Int64 Auc { get; set; }

        [DataMember(Name = "item")]
        public int Item { get; set; }

        [DataMember(Name = "owner")]
        public string Owner { get; set; }

        [DataMember(Name = "ownerRealm")]
        public string OwnerRealm { get; set; }

        [DataMember(Name = "bid")]
        public Int64 Bid { get; set; }

        [IgnoreDataMember]
        public Int64 BidMax { get; set; }

        [DataMember(Name = "buyout")]
        public Int64 BuyOut { get; set; }

        [DataMember(Name = "quantity")]
        public Int32 Quantity { get; set; }

        [DataMember(Name = "timeLeft")]
        public string FitstTimeLeft { get; set; }

        [DataMember(Name = "rand")]
        public Int32 Rand { get; set; }

        [DataMember(Name = "seed")]
        public Int64 Seed { get; set; }

        [DataMember(Name = "context")]
        public Int32 Context { get; set; }
    }
}
