using System;
using System.Collections.Generic;
using System.Linq;
using Asos.CodeTest.Configuation;

namespace Asos.CodeTest.FailoverServices
{
    public class FailoverService : IFailoverService
    {
        private readonly IFailoverRepository _failoverRepository;
        private readonly IConfigManager _configManager;
        
        public FailoverService(IFailoverRepository failoverRepository, IConfigManager configManager)
        {
            _failoverRepository = failoverRepository;
            _configManager = configManager;
        }

        public bool IsFailoverMode()
        {
            if (!_configManager.IsFailoverModeEnabled)
                return false;

            var failoverEntries = _failoverRepository.GetFailoverEntries();
            return GetFailoverRequestsCount(failoverEntries) > 100;
        }

        public int GetFailoverRequestsCount(IEnumerable<FailoverEntry> failoverEntries)
        {
            return failoverEntries.Count(failoverEntry => failoverEntry.DateTime > DateTime.Now.AddMinutes(-10));
        }
    }
}
