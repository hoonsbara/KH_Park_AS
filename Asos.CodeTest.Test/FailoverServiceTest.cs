using System;
using System.Collections;
using System.Collections.Generic;
using Asos.CodeTest.Configuation;
using Asos.CodeTest.FailoverServices;
using Moq;
using NUnit.Framework;

namespace Asos.CodeTest.Test
{
    [TestFixture]
    public class FailoverServiceTest
    {

        private Mock<IFailoverRepository> _failoverRepository;
        private Mock<IConfigManager> _configMock;
        private FailoverService _failoverService;

        [SetUp]
        public void Init()
        {
            _failoverRepository = new Mock<IFailoverRepository>();
            _configMock = new Mock<IConfigManager>();

            _failoverService = new FailoverService(_failoverRepository.Object, _configMock.Object);
        }
        
        private static IEnumerable TestCasesWithEntriesData()
        {
            yield return new TestCaseData(DateTime.Now.AddMinutes(-5), 101).Returns(101);
            yield return new TestCaseData(DateTime.Now.AddMinutes(-5), 99).Returns(99);
            yield return new TestCaseData(DateTime.Now.AddMinutes(-15), 150).Returns(0);
        }

        [TestCaseSource(typeof (FailoverServiceTest), "TestCasesWithEntriesData")]
        public int GetFailoverRequestsCount_RequestsCountTests(DateTime entryDate, int count)
        {
            // Arrange
            var entries = GetEntries(count, entryDate);

            // Act and Assert
            return _failoverService.GetFailoverRequestsCount(entries);
        }

        [Test]
        public void IsFailoverMode_FailoverModeFalse_ReturnFalse()
        {
            // Assert
            SetConfigManager(false);
            const bool expected = false;

            // Act
            var returned = _failoverService.IsFailoverMode();

            // Assert
            Assert.That(returned, Is.EqualTo(expected));
        }

        [Test]
        public void IsFailoverMode_RequestsLessThan100_ReturnFalse()
        {
            // Arrange
            var entries = GetEntries(50, DateTime.Now);
            SetFailoverRepositoryMock(entries);
            SetConfigManager(true);
            const bool expected = false;

            // Act
            var returned = _failoverService.IsFailoverMode();

            // Assert
            Assert.That(returned, Is.EqualTo(expected));
        }

        [Test]
        public void IsFailoverMode_RequestsMoreThan100_ReturnTrue()
        {
            // Arrange
            var entries = GetEntries(150, DateTime.Now);
            SetFailoverRepositoryMock(entries);
            SetConfigManager(true);
            const bool expected = true;

            // Act
            var returned = _failoverService.IsFailoverMode();

            // Assert
            Assert.That(returned, Is.EqualTo(expected));
        }

        private void SetFailoverRepositoryMock(List<FailoverEntry> entries)
        {
            _failoverRepository.Setup(x => x.GetFailoverEntries()).Returns(entries);
        }

        private void SetConfigManager(bool isFailoverModeEnabled)
        {
            _configMock.Setup(x => x.IsFailoverModeEnabled).Returns(isFailoverModeEnabled);
        }

        private List<FailoverEntry> GetEntries(int count, DateTime dateTime)
        {
            var failoverEntries = new List<FailoverEntry>();
            for (int i = 0; i < count; i++)
            {
                failoverEntries.Add(new FailoverEntry
                {
                    DateTime = dateTime
                });
            }
            return failoverEntries;
        }
    }
}