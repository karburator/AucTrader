using System;
using AucTrader.Logic.Models;

namespace AucTrader.Models
{
    public class UpdateModel
    {
        public string Status { get; set; }
        public IAucResponse Data { get; set; }
    }
}