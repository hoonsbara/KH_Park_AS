using System.Collections.Generic;

namespace Asos.CodeTest.FailoverServices
{
    public interface IFailoverRepository
    {
        List<FailoverEntry> GetFailoverEntries();
    }
}
