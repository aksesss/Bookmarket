using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mydz2_v2;
using System.Resources;
using System.Data;
using System.Collections.Generic;


namespace UnitTests
{
    [TestClass]
    public class TestCreateEvent
    {
        [TestMethod]
        public void TestMethodVClass()
        {
            string eventType = "TEST";
            string place = "someplace";
            string date = "01-01-2020";
            string time = "14:20";

            Error res = TransactionScripts.createEvent(eventType, place, date, time);

            Assert.IsTrue(!res.error && (res.message == "Событие успешно добавлено"));
        }
        [TestMethod]
        public void TestMethodInvClass()
        {
            string eventType = "TEST";
            string place = "someplace";
            string date = ("some string");
            string time = ("14:20");

            Error res = TransactionScripts.createEvent(eventType, place, date, time);

            Assert.IsTrue(res.error);
        }
    }

    [TestClass]
    public class TestGetEvent
    {
        /// <summary>
        ///id in DB 
        /// </summary>
        [TestMethod]
        public void TestMethodVClass()
        {
            int id = 2;
            DataTable dt = TransactionScripts.getEventById(id);
            bool res = (dt.Rows.Count != 0);
            Assert.IsTrue(res);
        }

        /// <summary>
        /// id > 0   and  id not in DB
        /// </summary>
        [TestMethod]
        public void TestMethodGetEventInvClass1()
        {
            int id = -2;
            DataTable dt = TransactionScripts.getEventById(id);
            bool res = (dt.Rows.Count == 0);
            Assert.IsTrue(res);
        }
        
        /// <summary>
        /// id <= 0
        /// </summary>
        [TestMethod]
        public void TestMethodGetEventInvClass2()
        {
            int id = -1;
            DataTable dt = TransactionScripts.getEventById(id);
            bool res = (dt.Rows.Count == 0);
            Assert.IsTrue(res);
        }

    }
    
    [TestClass]
    public class TestEncryption
    {
        [TestMethod]
        public void TestValidation()
        {
            string password = "testpassword";

            string hash = encryption.HashPassword(password);
            Assert.IsTrue(encryption.VerifyHashedPassword(hash, "testpassword"));
        }

        public void TestHasing()
        {
            string password = "testpassword";
            string hash1 = encryption.HashPassword(password);
            string hash2 = encryption.HashPassword(password);
            string hash3 = encryption.HashPassword(password);
            Assert.IsFalse((hash1 == hash2) || (hash1 == hash3) || (hash2 == hash3));
        }
    }
}
