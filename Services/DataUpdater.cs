using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseJournal.Services
{
    internal class DataUpdater
    {
        public delegate void DataUpdatedEventHandlert();
        public static event DataUpdatedEventHandlert DataUpdated;

        public static void InformAboutDataUpdateEvent()
        {
            DataUpdated?.Invoke();
        }
    }
}
