using System.Collections.Generic;

namespace Asos.CodeTest.FailoverServices
{
    public interface IFailoverService
    {
        bool IsFailoverMode();

        int GetFailoverRequestsCount(IEnumerable<FailoverEntry> failoverEntries);
    }

}
