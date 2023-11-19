using App;

namespace UnitTest
{
    [TestClass]
    public class HelperTest
    {
        [TestMethod]
        public void EllipsisTest()
        {
            Helper helper = new Helper();
            Assert.IsNotNull(helper, "new Helper() should not be null!");
            Assert.AreEqual("He...", helper.Ellipsis("Hello Mir epta", 5));
            Assert.AreEqual("Hel...", helper.Ellipsis("Hello Mir epta", 6));
            Assert.AreEqual("Test...", helper.Ellipsis("Test string", 7));
        }
        [TestMethod]
        public void FinalizeTest()
        {
            Helper helper = new();
            Assert.IsNotNull(helper, "new Helper() should not be null");
            Assert.AreEqual(
                "Hello, friend.",
                helper.Finalize("Hello, friend."));
        }
    }
}