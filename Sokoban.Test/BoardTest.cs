using NUnit.Framework;

namespace Alteridem.Sokoban.Test
{
   [TestFixture]
   public class BoardTest
   {
      private const string SIMPLE_BOARD = "#####\r\n#@$.#\r\n#####";
      private const string MEDIUM_BOARD = "#######\r\n#.@ # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######";

      [TestCase( "", "" )]
      [TestCase( SIMPLE_BOARD, SIMPLE_BOARD )]
      [TestCase( "#####\n#@$.#\n#####", SIMPLE_BOARD )]
      public void TestLoad( string boardStr, string expected )
      {
         var board = new Board();
         board.Load(boardStr);
         Assert.AreEqual( expected, board.ToString() );
      }

      [TestCase( "", Move.Down, false, "" )]
      [TestCase( SIMPLE_BOARD, Move.Down, false, "" )]
      [TestCase( SIMPLE_BOARD, Move.Up, false, "" )]
      [TestCase( SIMPLE_BOARD, Move.Right, true, "#####\r\n# @*#\r\n#####" )]
      [TestCase( SIMPLE_BOARD, Move.Left, false, "" )]
      [TestCase( MEDIUM_BOARD, Move.Up, false, "" )]
      [TestCase( MEDIUM_BOARD, Move.Down, true, "#######\r\n#.  # #\r\n#$+ $ #\r\n# $ $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      [TestCase( MEDIUM_BOARD, Move.Right, true, "#######\r\n#. @# #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      [TestCase( MEDIUM_BOARD, Move.Left, true, "#######\r\n#+  # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      public void TestMove( string boardStr, Move move, bool expectedValid, string expected )
      {
         var board = new Board();
         board.Load( boardStr );
         bool valid = board.MakeMove( move );
         Assert.AreEqual( expectedValid, valid );
         if (valid)
         {
            Assert.AreEqual(expected, board.ToString());
         }
         else
         {
            // Make sure it hasn't changed
            Assert.AreEqual(boardStr, board.ToString());
         }
      }
   }
}
