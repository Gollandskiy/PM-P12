using App;

namespace UnitTest
{
    [TestClass]
    public class HelperTest // Тестовый класс
    {
        [TestMethod] // Тестовые методы пошли
        public void EllipsisTest() // Обрезает строку
        {
            Helper helper = new Helper();
            Assert.IsNotNull(helper, "new Helper() should not be null!");
            Assert.AreEqual("He...", helper.Ellipsis("Hello Mir epta", 5));
            Assert.AreEqual("Hel...", helper.Ellipsis("Hello Mir epta", 6));
            Assert.AreEqual("Test...", helper.Ellipsis("Test string", 7));
        }
        [TestMethod]
        public void EllipsisExceptionTest() // Кидает исключения по определенным условиям
        {
            Helper helper = new();
            var ex =
                Assert.ThrowsException<ArgumentNullException>(
                    () => helper.Ellipsis(null!, 1)
                );
            Assert.IsTrue(
                ex.Message.Contains("input"),
                "Exception message should contain 'input' substring"
            );

            var ex2 = Assert.ThrowsException<ArgumentException>(
                () => helper.Ellipsis("Hello, world", 1)
            );
            Assert.IsTrue(ex2.Message.Contains("len"));

            var ex3 = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => helper.Ellipsis("Hello, world", 100)
            );
            Assert.IsTrue(ex3.Message.Contains("len"));
        }
        [TestMethod]
        public void ContainsAttributesTest()
        {
            Helper helper = new();
            Assert.IsNotNull(helper, "new Helper() should not be null");

            Assert.IsTrue(helper.ContainsAttributes("<div style=\"\"></div>"));
            Assert.IsTrue(helper.ContainsAttributes("<i style=\"code\" ></div>"));
            Assert.IsTrue(helper.ContainsAttributes("<i style=\"code\"  required ></div>"));
            Assert.IsTrue(helper.ContainsAttributes("<i style='code'  required></div>"));
            Assert.IsTrue(helper.ContainsAttributes("<i required style=\"code\" ></div>"));
            Assert.IsTrue(helper.ContainsAttributes("<i required style=\"code\"></div>"));
            Assert.IsTrue(helper.ContainsAttributes("<img onload=\"dangerCode()\" src=\"puc.png\"/>"));
            Assert.IsTrue(helper.ContainsAttributes("<img width=100 />"));
            Assert.IsTrue(helper.ContainsAttributes("<img width=100/>"));
            Assert.IsTrue(helper.ContainsAttributes("<img width=100>"));
            Assert.IsTrue(helper.ContainsAttributes("<img width=500 required/>"));
            Assert.IsTrue(helper.ContainsAttributes("<img      width=500    required   />"));

            Assert.IsFalse(helper.ContainsAttributes("<div></div>"));
            Assert.IsFalse(helper.ContainsAttributes("<div ></div>"));
            Assert.IsFalse(helper.ContainsAttributes("<br/>"));
            Assert.IsFalse(helper.ContainsAttributes("<br />"));
            Assert.IsFalse(helper.ContainsAttributes("<div required ></div>"));
            Assert.IsFalse(helper.ContainsAttributes("<div required>x=5</div>"));
            Assert.IsFalse(helper.ContainsAttributes("<div required checked></div>"));
            Assert.IsFalse(helper.ContainsAttributes("<div>2=2</div>"));
        }

        [TestMethod]
        public void EscapeHtmlTest()
        {
            Helper helper = new();

            Assert.IsNotNull(helper, "new Helper() should not be null");
            Assert.IsNotNull(helper.EscapeHtml(">"), "EscapeHtml should not return null!");
            Assert.IsNotNull(helper.EscapeHtml("<"), "EscapeHtml should not return null!");

            Assert.AreEqual(
                "&lt;div class=\"container\"&gt;&lt;p&gt;Hello, &amp; world!&lt;/p&gt;&lt;/div&gt;",
                helper.EscapeHtml("<div class=\"container\"><p>Hello, & world!</p></div>")
            );
            Assert.AreEqual("&lt;Hello world!&gt;", helper.EscapeHtml("<Hello world!>"));
            Assert.AreEqual("&lt;p&gt;Hello &amp; Goodbye&lt;/p&gt;", helper.EscapeHtml("<p>Hello & Goodbye</p>"));
            Assert.AreEqual("", helper.EscapeHtml(""));
        }
        [TestMethod]
        public void EscapeHtmlExceptionTest()
        {
            Helper helper = new();
            Assert.IsNotNull(helper, "new Helper() should not be null");

            var ex = Assert.ThrowsException<ArgumentException>(
                () => helper.EscapeHtml(null!)
            );
            Assert.AreEqual("Argument 'html' is null", ex.Message);
        }

        [TestMethod]
        public void FinalizeTest() // Дополняет строку, если в конце нету точки
        {
            Helper helper = new();
            Assert.IsNotNull(helper, "new Helper() should not be null");
            Assert.AreEqual(
                "Hello, friend.",
                helper.Finalize("Hello, friend."));
        }
        [TestMethod]
        public void CombineUrlTest() // Сочетает между собой строки в одну
        {
            Helper helper = new();

            Dictionary<string[], string> testCases = new()
            {
                { new[] { "/home", "index" }, "/home/index" },
                { new[] { "/shop", "/cart" }, "/shop/cart" },
                { new[] { "auth/", "logout" }, "/auth/logout" },
                { new[] { "forum/", "topic/" }, "/forum/topic" },
                { new[] { "/home///", "index" }, "/home/index" },
                { new[] { "///home/", "/index" }, "/home/index" },
                { new[] { "home/", "////index" }, "/home/index" },
                { new[] { "///home/////", "////index///" }, "/home/index" },
            };
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    testCase.Value,
                    helper.CombineUrl(testCase.Key[0], testCase.Key[1]),
                    $"{testCase.Key[0]} - {testCase.Key[1]}"
                );
            }
        }
        [TestMethod]
        public void CombineUrlMultiTest() // Делает практически то же самое что и CombineUrlTest
        {
            Helper helper = new();
            Dictionary<String[], String> testCases = new()
            {
                 { new[] { "/home", "/index", "/gmail" }, "/home/index/gmail" },
                { new[] { "/shop", "/cart/", "com" }, "/shop/cart/com" },
                { new[] { "auth/", "logout" }, "/auth/logout" },
                { new[] { "forum", "topic/", "/com/" }, "/forum/topic/com" },
                { new[] { "//forum////", "topic////", "///com" }, "/forum/topic/com" },
                { new[] { "forum", "topic", "com" }, "/forum/topic/com" },
                { new[] { "/forum/", "/topic///////////", "//com////////////////" }, "/forum/topic/com" },
                { new[] { "/shop", "/cart", "/user", "..", "/123" }, "/shop/cart/123" },
                { new[] { "/shop///", "///cart", "user", "..", "////123///" }, "/shop/cart/123" },
                { new[] { "/shop///", "///cart", "user", "..", "////123///", "456" }, "/shop/cart/123/456" },
                { new[] { "/shop///", "///cart", "..", "user//", "///123", "456//" }, "/shop/user/123/456" },
            };
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    testCase.Value,
                    helper.CombineUrl(testCase.Key),
                    $"{testCase.Value} -- {testCase.Key[0]} + {testCase.Key[1]}"
                );
            }
        }
        [TestMethod]
        public void CombineUrlExceptionTest() // Кидает исключения с использованием CombineUrl
        {
            Helper helper = new();
            Assert.AreEqual("/home", helper.CombineUrl("/home", null!));
            Assert.AreEqual("/home/path", helper.CombineUrl("/home", "///path//", null!));
            Assert.AreEqual("/home/user", helper.CombineUrl("/home", "///path//", "..", "user//", null!));

            var ex = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, null!)
            );
            Assert.AreEqual("All arguments are null", ex.Message);

            ex = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, null!, null!, null!, null!, null!)
            );
            Assert.AreEqual("All arguments are null", ex.Message);

            ex = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl()
            );
            Assert.AreEqual("Parts is empty", ex.Message);

            var ex2 = Assert.ThrowsException<NullReferenceException>(
                () => helper.CombineUrl(null!)
            );
            Assert.AreEqual("Parts is null", ex2.Message);

            var ex3 = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, "/subsection")
            );
            Assert.AreEqual("Non-Null argument after Null one", ex3.Message);

            ex3 = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl("/path", null!, "/subsection")
            );
            Assert.AreEqual("Non-Null argument after Null one", ex3.Message);

            ex3 = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl("/path", "/path2", null!, "/subsection")
            );
            Assert.AreEqual("Non-Null argument after Null one", ex3.Message);
        }
    }
}