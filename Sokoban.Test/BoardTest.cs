using NUnit.Framework;

namespace Alteridem.Sokoban.Test
{
   [TestFixture]
   public class BoardTest
   {
      [TestCase( "#####\r\n#@$.#\r\n#####", 1, 1, '@' )]
      [TestCase( "#####\n#@$.#\n#####", 1, 1, '@' )]
      public void TestLoad( string boardStr, int x, int y, char c )
      {
         var board = new Board();
         board.Load(boardStr);
         Assert.AreEqual( c, board.Squares[x,y] );
      }
   }
}
