using MMABooksBusinessClasses;
using MMABooksDBClasses;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Org.BouncyCastle.Asn1.Sec;
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
            // testing what type of error we see when it's out of range
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
            Console.WriteLine($"Customer ID: {c.CustomerID}");

            // delete user (maintains reference to the object c)
            CustomerDB.DeleteCustomer(c);

            // CustomerDB is setup to return null
            // if the DataReader object's .Read() method
            // doesn't return true, so we'll expect a null value
            ClassicAssert.IsNull(CustomerDB.GetCustomer(1));

            // readd that same customer to leave the DB unaltered for testing
            int readdID = CustomerDB.AddCustomer(c);
            Console.WriteLine($"Added customer, customerID: {readdID}");
            Customer c2 = CustomerDB.GetCustomer(readdID);

            // update the readded customer to fix the customer ID
            CustomerDB.UpdateCustomer(c, c2);

            // confirm that the name matches the name from
            // the since-deleted customer we pulled earlier
            Console.WriteLine($"Deleted customer: {c.Name}\nReadded customer: {c2.Name}");
            ClassicAssert.AreEqual(c.Name, c2.Name);
        }
    }
}
