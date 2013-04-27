using NUnit.Framework;

namespace InfiniteIsland.Test
{   
    [TestFixture]
    //NUnit Quick Guide: http://www.codeproject.com/Articles/178635/Unit-Testing-Using-NUnit
    public class UnitTest1  //TODO Rename class.
    {
        [TestCase]
        public void ShouldPass()
        {
            Assert.AreEqual(true, true);
        }
    }
}