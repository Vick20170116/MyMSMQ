using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMSMQSender;
using MyMSMQReceiver;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int sendCount = MyMSMQSender.Program.SendMessageAll();
            Assert.AreEqual(sendCount, 10);
        }

        [TestMethod]
        public void TestMethod2()
        {
            int sendCount = MyMSMQSender.Program.SendMessageAll();
            Assert.AreEqual(sendCount>0, true);

            int receiveCount = MyMSMQReceiver.Program.ReceiveMessage();
            Assert.AreEqual(receiveCount>0, true);

        }
    }
}
