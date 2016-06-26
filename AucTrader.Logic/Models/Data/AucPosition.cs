using System;
using System.Runtime.Serialization;
using AucTrader.Logic.Models.DataBase;

namespace AucTrader.Logic.Models
{
    [DataContract]
    public class AucPosition
    {
        [DataMember(Name = "auc")]
        public Int64 Auc { get; set; }

        [DataMember(Name = "item")]
        public Int32 Item { get; set; }

        [DataMember(Name = "owner")]
        public string Owner { get; set; }

        [DataMember(Name = "ownerRealm")]
        public string OwnerRealm { get; set; }

        [DataMember(Name = "did")]
        public Int64 Bid { get; set; }

        [DataMember(Name = "buyOut")]
        public Int64 BuyOut { get; set; }

        [DataMember(Name = "quantity")]
        public Int32 Quantity { get; set; }

        [DataMember(Name = "timeLeft")]
        public string TimeLeft { get; set; }

        [DataMember(Name = "rand")]
        public Int32 Rand { get; set; }

        [DataMember(Name = "seed")]
        public Int64 Seed { get; set; }

        [DataMember(Name = "context")]
        public Int32 Context { get; set; }

        [IgnoreDataMember]
        public int Id { get; set; }

        [IgnoreDataMember]
        public DateTime LoadDateTime { get; set; }
    }
}
