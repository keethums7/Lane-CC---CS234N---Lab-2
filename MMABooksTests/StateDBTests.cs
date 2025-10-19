using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Legacy;
using MMABooksBusinessClasses;
using MMABooksDBClasses;

using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    [TestFixture]
    public class StateDBTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestGetStates()
        {
            List<State> states = StateDB.GetStates();
            /* Added "using NUnit.Framework.Legacy" to the top level declarations
             * as I was getting an error showing "'Assert' does not contain a definition for 'AreEqual'
             * and after looking into it, it seems like newer versions of the NUnit framework
             * have changed the methods associated with 'Assert'
             * Apologies if this is an issue, I will be reaching out via email to confirm if
             * there's a better way to do this. Just wanted to complete the assignment on time.
             */
            ClassicAssert.AreEqual(53, states.Count);
            ClassicAssert.AreEqual("Alabama", states[0].StateName);
        }

        [Test]
        public void TestGetStatesDBUnavailable()
        {
            Assert.Throws<MySqlException>(() => StateDB.GetStates());
        }
    }
}
