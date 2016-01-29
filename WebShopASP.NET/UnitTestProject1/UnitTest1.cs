using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanAddTwoNumbersTogether()
        {
            var sum = 1 + 2;
            Assert.AreEqual(3, sum);
        }
        public void CanAddThreeNumberTogether()
        {
        }
        
    }


}
