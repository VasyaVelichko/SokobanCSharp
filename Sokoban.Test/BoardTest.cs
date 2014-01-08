using System;
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

      [TestCase( "", Move.Down, false, "" )]
      [TestCase( "#####\r\n#@$.#\r\n#####", Move.Down, false, "" )]
      [TestCase( "#####\r\n#@$.#\r\n#####", Move.Up, false, "" )]
      [TestCase( "#####\r\n#@$.#\r\n#####", Move.Right, true, "#####\r\n# @*#\r\n#####" )]
      [TestCase( "#####\r\n#@$.#\r\n#####", Move.Left, false, "" )]
      [TestCase( "#######\r\n#.@ # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######", Move.Up, false, "" )]
      [TestCase( "#######\r\n#.@ # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######", Move.Down, true, "#######\r\n#.  # #\r\n#$+ $ #\r\n# $ $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      [TestCase( "#######\r\n#.@ # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######", Move.Right, true, "#######\r\n#. @# #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
      [TestCase( "#######\r\n#.@ # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######", Move.Left, true, "#######\r\n#+  # #\r\n#$* $ #\r\n#   $ #\r\n# ..  #\r\n#  *  #\r\n#######" )]
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
