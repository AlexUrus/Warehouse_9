using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseJournal.Model
{
    public class ItemType
    {
        public string Type { get; set;}

        public static List<ItemType> Types = new List<ItemType>()
        {
            new("Без типа"),
            new("Молочная продукция"),
            new("Бакалея")
        };

        private ItemType(string type) 
        {
            Type = type;
        }
    }
}
