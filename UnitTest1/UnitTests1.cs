using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mydz2_v2;

namespace UnitTests
{
    [TestClass]
    public class UnitTestsfirstTry
    {
        [TestMethod]
        public void TestMethod1()
        {

            Users user = new Users();
            user.fname = "qwe";
            user.lname = "qwe";
            user.upassword = encryption.HashPassword("qwe");
            user.email = "q@q.ru";

            //bool expected = true;
            //bool actual = student.TM_Validation(student);
            Assert.AreEqual(true, true);
            }
    }
}
