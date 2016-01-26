using System.Collections.Generic;

namespace Asos.CodeTest.FailoverServices
{
    public class FailoverRepository : IFailoverRepository
    {
        public List<FailoverEntry> GetFailoverEntries()
        {
            // return all from fail entries from database
            return new List<FailoverEntry>();
        } 
    }
}