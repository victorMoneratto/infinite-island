using NUnit.Framework;

namespace DasGameTest
{
    [TestFixture]
    public class UnitTest1  //RENAME THIS
    {
        [TestCase]
        public void ShouldPass()
        {
            //Quick Guide: http://www.codeproject.com/Articles/178635/Unit-Testing-Using-NUnit
            Assert.AreEqual(true, true);
        }
    }
}