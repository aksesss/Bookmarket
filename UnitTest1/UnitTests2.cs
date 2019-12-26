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
            string date = ("01-01-2020");
            string time = ("14:20");

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
        [TestMethod]
        public void TestMethodVClass()
        {
            int id = 2;
            DataTable dt = TransactionScripts.getEventById(id);
            bool res = (dt != null);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestMethodGetEventInvClass()
        {
            int id = -1;
            DataTable dt = TransactionScripts.getEventById(id);
            bool res = (dt.Rows.Count == 0);
            Assert.IsTrue(res);
        }

    }

    public class TestEncryption
}
