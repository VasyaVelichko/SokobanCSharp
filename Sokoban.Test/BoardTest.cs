using NUnit.Framework;

namespace Alteridem.Sokoban.Test
{
   [TestFixture]
   public class BoardTest
   {
      [TestCase( "", "" )]
      [TestCase( "#####\r\n#@$.#\r\n#####", "#####\r\n#@$.#\r\n#####" )]
      [TestCase( "#####\n#@$.#\n#####", "#####\r\n#@$.#\r\n#####" )]
      public void TestLoad( string boardStr, string expected )
      {
         var board = new Board();
         board.Load(boardStr);
         Assert.AreEqual( expected, board.ToString() );
      }
   }
}
