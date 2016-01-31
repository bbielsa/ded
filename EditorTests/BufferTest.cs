using Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EditorTests
{
    [TestClass]
    public class BufferTest
    {
        [TestMethod]
        public void TestStringConstructor()
        {
            var buf1 = new Buffer("");
            var buf2 = new Buffer("hello");
            var buf3 = new Buffer("hello\nbanana");
            var buf4 = new Buffer("hello\nbanana\n");
            var buf5 = new Buffer("hello\r\nbanana");
            var buf6 = new Buffer("hello\r\nbanana\r\n");
            var buf7 = new Buffer("hello\nbanana\r\nx");
            var buf8 = new Buffer("hello\r\nbanana\nx");
            var buf9 = new Buffer("hello\r");
            Assert.AreEqual(buf1.ToString(), "");
            Assert.AreEqual(buf1.ToString(), "");
            Assert.AreEqual(buf2.ToString(), "hello");
            Assert.AreEqual(buf3.ToString(), "hello\nbanana");
            Assert.AreEqual(buf4.ToString(), "hello\nbanana\n");
            Assert.AreEqual(buf5.ToString(), "hello\r\nbanana");
            Assert.AreEqual(buf6.ToString(), "hello\r\nbanana\r\n");
            Assert.AreEqual(buf7.ToString(), "hello\nbanana\r\nx");
            Assert.AreEqual(buf8.ToString(), "hello\r\nbanana\nx");
            Assert.AreEqual(buf9.ToString(), "hello\r");
        }
    }
}
