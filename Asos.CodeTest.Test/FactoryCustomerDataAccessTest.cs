using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asos.CodeTest.DataAccessServices;
using NUnit.Core;
using NUnit.Framework;

namespace Asos.CodeTest.Test
{
    [TestFixture]
    public class FactoryCustomerDataAccessTest
    {
        [Test]
        public void GetCustomerDataAccessObject_IsFailoverModeTrue_ReturnFailoverCustomerDataAccessTypeOfObject()
        {
            // Assert
            var factoryCustomerDataAccess = new FactoryCustomerDataAccess();
            var expected = typeof(FailoverCustomerDataAccess);

            // Act
            var returned = factoryCustomerDataAccess.GetCustomerDataAccessObject(true);

            // Assert
            Assert.That(returned.GetType(), Is.EqualTo(expected));
        }

        [Test]
        public void GetCustomerDataAccessObject_IsFailoverModeFalse_ReturnCustomerDataAccessTypeOfObject()
        {
            // Assert
            var factoryCustomerDataAccess = new FactoryCustomerDataAccess();
            var expected = typeof(CustomerDataAccess);

            // Act
            var returned = factoryCustomerDataAccess.GetCustomerDataAccessObject(false);

            // Assert
            Assert.That(returned.GetType(), Is.EqualTo(expected));
        }
    }
}
