using Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EditorTests
{
    [TestClass]
    public class CursorTest
    {
        private Buffer buf = new Buffer("hello\nbanana\naxe");

        [TestMethod]
        public void TestMoveUp()
        {
            Assert.AreEqual(new Cursor(0, 2).Move(buf, MoveDirection.Up), new Cursor(0, 0));
            Assert.AreEqual(new Cursor(1, 6).Move(buf, MoveDirection.Up), new Cursor(0, 5));
        }

        [TestMethod]
        public void TestMoveDown()
        {
            Assert.AreEqual(new Cursor(1, 6).Move(buf, MoveDirection.Down), new Cursor(2, 3));
            Assert.AreEqual(new Cursor(2, 1).Move(buf, MoveDirection.Down), new Cursor(2, 3));
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            Assert.AreEqual(new Cursor(0, 0).Move(buf, MoveDirection.Left), new Cursor(0, 0));
            Assert.AreEqual(new Cursor(0, 2).Move(buf, MoveDirection.Left), new Cursor(0, 1));
            Assert.AreEqual(new Cursor(1, 0).Move(buf, MoveDirection.Left), new Cursor(0, 5));
        }

        [TestMethod]
        public void TestMoveRight()
        {
            Assert.AreEqual(new Cursor(2, 3).Move(buf, MoveDirection.Right), new Cursor(2, 3));
            Assert.AreEqual(new Cursor(0, 1).Move(buf, MoveDirection.Right), new Cursor(0, 2));
            Assert.AreEqual(new Cursor(1, 6).Move(buf, MoveDirection.Right), new Cursor(2, 0));
        }
    }
}
