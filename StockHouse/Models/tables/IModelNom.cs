using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockHouse.Models.tables
{
    public interface IModelNom : IModelBase
    {
        string Nom { get; set; }
    }
}