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

      [TestCase( "", Move.Down, false )]
      [TestCase( "#####\n#@$.#\n#####", Move.Down, false )]
      [TestCase( "#####\n#@$.#\n#####", Move.Up, false )]
      [TestCase( "#####\n#@$.#\n#####", Move.Right, true )]
      [TestCase( "#####\n#@$.#\n#####", Move.Left, false )]
      [TestCase( "#######\n#.@ # #\n#$* $ #\n#   $ #\n# ..  #\n#  *  #\n#######", Move.Up, false )]
      [TestCase( "#######\n#.@ # #\n#$* $ #\n#   $ #\n# ..  #\n#  *  #\n#######", Move.Down, true )]
      [TestCase( "#######\n#.@ # #\n#$* $ #\n#   $ #\n# ..  #\n#  *  #\n#######", Move.Right, true )]
      [TestCase( "#######\n#.@ # #\n#$* $ #\n#   $ #\n# ..  #\n#  *  #\n#######", Move.Left, true )]
      public void TestValidMove( string boardStr, Move move, bool expected )
      {
         var board = new Board( );
         board.Load( boardStr );
         bool valid = board.IsMoveValid( move );
         Assert.AreEqual( expected, valid );
      }

      [TestCase( "#####\r\n#@$.#\r\n#####", Move.Right, "#####\r\n# @*#\r\n#####" )]
      [TestCase( "#######\r\n#.@ # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######", Move.Down, "#######\r\n#.  # #\r\n#$+ $ #\r\n# $ $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      [TestCase( "#######\r\n#.@ # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######", Move.Right, "#######\r\n#. @# #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      [TestCase( "#######\r\n#.@ # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######", Move.Left, "#######\r\n#+  # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      public void TestMove( string boardStr, Move move, string expected )
      {
         var board = new Board();
         board.Load( boardStr );
         bool valid = board.MakeMove( move );
         Assert.IsTrue( valid );
         Assert.AreEqual( expected, board.ToString() );
      }
   }
}
