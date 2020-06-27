using ODLSystem.Library.Common.EntityModel;
using ODLSystem.PersistenceLayer.Repository.PLCEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.BusinessLayer.PLCEntry
{
    public class PLCEntryBusinessLayer
    {
        PLCEntryPersistenceLayer pLCEntryPersistenceLayer;
        public PLCEntryBusinessLayer()
        {
            pLCEntryPersistenceLayer = new PLCEntryPersistenceLayer();
        }

        public bool insertPLCEntry(PLCEntityModel pLCEntityModel)
        {
            bool result = false;

            try
            {
                result = pLCEntryPersistenceLayer.insertPLCEntry(pLCEntityModel);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public List<MasterPilotZone> GetPilotList()
        {
            try
            {
                return pLCEntryPersistenceLayer.GetPilotList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
