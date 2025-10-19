using MMABooksBusinessClasses;
using MMABooksDBClasses;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMABooksTests
{
    public class CustomerDBTests
    {

        [Test]
        public void TestGetCustomer()
        {
            Customer c = CustomerDB.GetCustomer(1);
            ClassicAssert.AreEqual(1, c.CustomerID);
        }

        [Test]
        public void TestCreateCustomer()
        {
            Customer c = new Customer();
            c.Name = "Mickey Mouse";
            c.Address = "101 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "10101";

            int customerID = CustomerDB.AddCustomer(c);
            c = CustomerDB.GetCustomer(customerID);
            ClassicAssert.AreEqual("Mickey Mouse", c.Name);
        }

        [Test]
        public void TestDeleteCustomer()
        {
            Customer c = CustomerDB.GetCustomer(1);
            Console.WriteLine($"Customer Name: {c.Name}");
            // delete user (maintains reference to the object c)
            CustomerDB.DeleteCustomer(c);
            // check to confirm the customer with that ID
            // throws an error when we run a select statement (retrieve)
            Assert.Throws<MySqlException>(() => CustomerDB.GetCustomer(1));
            // readd that same customer to leave the DB unaltered for testing
            int customerID = CustomerDB.AddCustomer(c);
            // confirm that the customerID matches the ID we pulled earlier
            Assert.Equals(c.CustomerID, customerID);
        }
    }
}
